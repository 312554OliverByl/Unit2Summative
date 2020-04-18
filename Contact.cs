/*
 * Oliver Byl
 * April 17, 2020
 * Unit 2 Summative (Contact Form Editor)
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Unit2Summative
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Displays a MessageBox with the contact's current age.
        /// </summary>
        public void ShowAge()
        {
            // Calculate age using DateTime's arithmetic.
            TimeSpan age = DateTime.UtcNow - Birthday;

            // age.ToString() yields several terms seperated by colons.
            // The first term is age in days.
            // Parse this value into a double to calculate years.
            double.TryParse(age.ToString().Split(':')[0], out double days);
            
            // The purpose of the extra math around "days / 365" is to
            // keep the age in years to two decimal places without rounding.
            double years = Math.Truncate((days / 365) * 100) / 100;

            MessageBox.Show("This user is " + years + " years old.");
        }

        /// <summary>
        /// Reads a list of contacts from "contact.txt" in the same folder
        /// as the executable.
        /// </summary>
        /// <returns>A list of the parsed contacts.</returns>
        public static List<Contact> ReadFromFile()
        {
            List<Contact> result = new List<Contact>();

            try
            {
                StreamReader reader = new StreamReader("contact.txt");

                while(!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    // Get all values from comma-seperated line as strings.
                    string[] items = line.Split(',');

                    // Parse out year, month, and day components of birth date.
                    int.TryParse(items[2], out int year);
                    int.TryParse(items[3], out int month);
                    int.TryParse(items[4], out int day);

                    result.Add(new Contact() { FirstName = items[0], LastName = items[1], Birthday = new DateTime(year, month, day), Email = items[5] });
                }

                reader.Close();
            }
            catch(Exception)
            {
                // This would trigger, for example, if a contact's birthday could not be parsed.
                string message = "contaxt.txt contains malformed data." + Environment.NewLine
                    + "Each line should be in the format \'[FirstName],[LastName],[YearBorn],[MonthBorn],[DayBorn],[Email]\'";
                MessageBox.Show(message);
            }

            return result;
        }

        /// <summary>
        /// Saves a list of contacts to contact.txt in proper CSV format.
        /// </summary>
        /// <param name="contacts">The list of contacts to save.</param>
        public static void SaveToFile(List<Contact> contacts)
        {
            /* Do not save if there are no contacts.
             * 
             * This would happen, for example, if there was malformed data and
             * loading was aborted. The absense of this check would then cause all
             * data in the file to be deleted (overwritten with the current, empty, list).
             */
            if (contacts.Count == 0)
                return;

            string output = "";

            foreach(Contact contact in contacts)
            {
                output += contact.FirstName + ","
                        + contact.LastName + ","
                        + contact.Birthday.Year + ","
                        + contact.Birthday.Month + ","
                        + contact.Birthday.Day + ","
                        + contact.Email + Environment.NewLine;
            }

            output.Trim();

            StreamWriter writer = new StreamWriter("contact.txt");

            try
            {
                writer.Write(output);
                writer.Flush();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            writer.Close();
        }
    }
}

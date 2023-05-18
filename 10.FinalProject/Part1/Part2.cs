using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Part2
{

    
    class EmployeeRecord
    {
        // create an employee Record with public properties
        //    ID                        a number to identify an employee
        //    Name                      the employee name
        //    StateCode                 the state collecting taxes for this employee
        //    HoursWorkedInTheYear      the total number of hours worked in the entire year (including fractions of an hour)
        //    HourlyRate                the rate the employee is paid for each hour worked
        //                                  assume no changes to the rate throughout the year.

        //    provide a constructor that takes a csv and initializes the employeerecord
        //       do all error checking and throw appropriate exceptions

        //    provide an additional READ ONLY property called  YearlyPay that will compute the total income for the employee
        //        by multiplying their hours worked by their hourly rate

        //    provide an additional READONLY property that will compute the total tax due by:
        //        calling into the taxcalculator providing the statecode and the yearly income computed in the YearlyPay property

        //    provide an override of toString to output the record : including the YearlyPay and the TaxDue

        public int EmId { get; set; }
        public string EmName { get; set; }
        public string StateCode { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal HoursWorkedInTheYear { get; set; }
        public EmployeeRecord(string csv){
            const char Separator = ',';

            string[] data = csv.Split(Separator);
            if (5 != data.Length)
            {
                throw new Exception($"Not enough arguments for constructor");
            }

            EmId = int.Parse(data[0]);
            EmName = data[1];
            if (data.Length == 2) { throw new Exception($"Only 2 characters needed for State."); }
            StateCode = data[2];

            decimal temp;
            if (!decimal.TryParse(data[3], out temp)) { throw new Exception($"{data[3]}; Not a deciaml"); }
            HoursWorkedInTheYear = temp;

            if (!decimal.TryParse(data[4], out temp)) { throw new Exception($"{data[4]}; Not a decimal"); }
            HourlyRate = temp;


        }
        public decimal PaidYear { get { return this.HoursWorkedInTheYear * this.HourlyRate; } }
        public decimal Taxtotal { get { return Part1.TaxCalculator.ComputeTaxFor(StateCode, PaidYear); } }

    }

    static class EmployeesList
    {

        // create an EmployeeList class that will read all the employees from the Employees.csv file
        // the logic is similar to the way the taxcalculator read its taxrecords

        // Create a List of employee records.  The employees are arranged into a LIST not a DICTIONARY
        //   because we are not accessing the employees by state,  we are accessing the employees sequentially as a list

        // create a static constructor to load the list from the file
        //   be sure to include try/catch to display messages
        public static List<EmployeeRecord> Employee = new List<EmployeeRecord>();
        static EmployeesList()
        {
            string file2 = @"employees.csv";
            try
            {
                using (StreamReader read = new StreamReader(file2)){


                    foreach (string line in File.ReadLines(file2))
                    {
                        try{
                            EmployeeRecord emp = new EmployeeRecord(line);
                            Employee.Add(emp);


                        }
                        catch (Exception ex){
                            Console.WriteLine(ex.Message);
                        }
                    }
                    read.Close();

                }
            }
            catch (Exception ex) {
                Console.WriteLine($"ERORR:{ex.Message}");
            }
        }
    
}






    class Program
    {

        // loop over all the employees in the EmployeeList and print them
        // If the ToString() in the employee record is correct, all the data will print out.

        public static void Main()
        {
        try
        { // write your logic here
            foreach (EmployeeRecord emp in EmployeesList.Employee)
            {

                Console.WriteLine($"Employee ID:{emp.EmId} \t Employee Name: {emp.EmName}" +
                    $"\nState ID:{emp.StateCode} \t Yearly Pay{emp.PaidYear:0.00}\nTax Due {emp.Taxtotal:0.00}" +
                    $"\t Hourly Rate{emp.HourlyRate:0.00}\n Yearly hours worked:{emp.HoursWorkedInTheYear:0.00}");
            }

        }


        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    }
}


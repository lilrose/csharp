using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//Chantal
namespace Part1
{

    class TaxRecord{

        // create a TaxRecord class representing a line from the file.  
        // It should have public properties of the correct type
        // for each of the columns in the file
        //  StateCode   (used as the key to the dictionary)
        //  State       (Full state name)
        //  Floor       (lowest income for this tax bracket)
        //  Ceiling     (highest income for this tax bracket )
        //  Rate        (Rate at which income is taxed for this tax bracket)
        //
        //  Create a ctor taking a single string (a csv) and use it to load the properties in the record
        //  Be sure to throw detailed exceptions when the data is invalid
        //
        //  Create an override of ToString to print out the tax record info nicely
           //  Create a ctor taking a single string (a csv) and use it to load the record
            //  Be sure to throw detailed exceptions when the data is invalid
            //

            public string Statecode { get; set; }
            public string Nameofstate { get; set; }
            public decimal lowIncome { get; set; }
            public decimal highIncome { get; set; }
            public decimal Rate { get; set; }
   

        public TaxRecord(string csv)
        {
            const char Separator = ',';

            string[] data = csv.Split(Separator);
            if (5 != data.Length)
            {
                throw new Exception($"CSV file {csv} needs five values(state ID," +
                    $" state,minimum income, maxium income, rate)");
            }

            Statecode = data[0];
            Nameofstate = data[1];
            decimal temp1;

            if (!decimal.TryParse(data[2], out temp1)) { throw new Exception($"{csv} Needs to be a decimal form."); }
            lowIncome = temp1;

            if (!decimal.TryParse(data[3], out temp1)) { throw new Exception($"{csv} Needs to be a decimal form."); }
            highIncome = temp1;

            if (!decimal.TryParse(data[4], out temp1)) { throw new Exception($"{csv} Needs to be a decimal form."); }
            Rate = temp1;
        } //this is the end of taxrecord
        //  Create an override of ToString to print out the tax record info nicely
        public override string ToString()
        { 

            return $"State ID:{Statecode} State:{Nameofstate} Rate:{Rate} Lowincome: {lowIncome,15}" +
                $"Hghincome{highIncome,15}";
        }
    




    }  // this is the end of the TaxRecord


    static class TaxCalculator
    {
        // Create a static dictionary field that holds a List of TaxRecords and is keyed by a string
        private static Dictionary<string, List<TaxRecord>> record = new Dictionary<string, List<TaxRecord>>();

        // create a static constructor that:
        static TaxCalculator() {
            string filename = @"taxtable.csv";
            try {
                using (StreamReader read = new StreamReader(filename)) {

                    foreach (string line in File.ReadLines(filename)) {
                        try {
                            List<TaxRecord> records;
                            TaxRecord tax = new TaxRecord(line);
                            bool ispresent = record.TryGetValue(tax.Statecode, out records);
                            if (ispresent) { records.Add(tax); }

                            else {
                                records = new List<TaxRecord>();
                                records.Add(tax);
                                record.Add(tax.Statecode, records);
                            }
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
        public static decimal ComputeTaxFor(string statAbv, decimal incomeTaxEarn)
        {
            decimal finaltax = 0M;

            List<TaxRecord> codename;
            decimal totaltax = 0M;
            try {
                if (record.TryGetValue(statAbv.ToUpper(), out codename)) {
                    foreach (var records in codename) {
                        if (incomeTaxEarn > records.highIncome)
                        {
                            totaltax = (records.highIncome - records.lowIncome) * records.Rate;
                            finaltax += totaltax;
                        }

                        if (incomeTaxEarn < records.highIncome && incomeTaxEarn > records.lowIncome) {
                            totaltax = (incomeTaxEarn - records.lowIncome) * records.Rate;
                            finaltax += totaltax;
                            return finaltax;
                        }

                    }

                }
            }
            catch (Exception) { Console.WriteLine($"Invalid statecode:{statAbv}"); }

            return finaltax;
        }
        // declare a streamreader to read a file
        // enter a try/catch block for the entire static constructor to print out a message if an error occur
        // initialize the static dictionary to a newly create empty one
        // open the taxtable.csv file into the streamreader
        // loop over the lines from the streamreader
        // read a line from the file
        // constuct a taxrecord from the (csv) line in the file
        // see if the state in the taxrecord is already in the dictionary
        //     if it is:  add the new tax record to the list of records in that state
        //     if it is not
        //            create a new list of taxrecords
        //            add the new taxrecord to the list
        //            add the list to the dictionary under the state for the taxrecord
        //provide a way to get out of the loop when you are done with the file....
        // catch any exceptions while processing each line in another try/catch block located INSIDE the loop
        //   this way if the line in the CSV file is incorrect, you will continue to process the next line
        // make sure the streamreader is disposed no matter what happens.


        // create a static method (ComputeTaxFor)  to return the computed tax given a state and income
        //  use the state as a key to find the list of taxrecords for that state
        //   throw an exception if the state is not found.
        //   otherwise use the list to compute the taxes

        //  Create a variable to hold the final computed tax.  set it to 0
        //  loop over the list of taxrecords for the state
        //     check to see if the income is within the tax bracket using the floor and ceiling properties in the taxrecord
        //     if NOT:  (the income is greater than the ceiling)
        //        compute the total tax for the bracket and add it to the running total of accumulated final taxes
        //        the total tax for the bracket is the ceiling minus the floor times the tax rate for that bracket.  
        //        all this information is located in the taxrecord
        //        after adding the total tax for this bracket, continue to the next iteration of the loop
        //     IF The income is within the tax bracket (the income is higher than the floor and lower than the ceiling
        //        compute the final tax by adding the tax for this bracket to the accumulated taxes
        //        the tax for this bracket is the income minus the floor time the tax rate for this bracket
        //        this number is the total final tax, and can be returned as the final answer


        // try to figure out how to implement the Verbose option AFTER you have everything else done.


    }  // this is the end of the Tax Calculator

    
   

    class Program
    {
        public static void Main()
        {
            // create an infinite loop to:
            // prompt the user for a state and an income
            // validate the data
            // calculate the tax due and print out the total
            // loop

            // after accomplishing this, you may want to also prompt for verbose mode or not in this loop
            // wrap everythign in a try/catch INSIDE the loop.  print out any exceptions that are unhandled
            //  something like this:
            // prompt the user for a state and an income
            // calculate the tax due and print out the total
            // loop
            do
            {
            decimal tax = 0;
            string temp;
            decimal incomeTaxEarn = 0;
            string statAbv;
          
            Console.WriteLine($"Enter state:");
                statAbv = Console.ReadLine();
            Console.WriteLine($"Enter Income:");
            temp = Console.ReadLine();
            try {
                    incomeTaxEarn = decimal.Parse(temp);

            }
            catch (Exception) {
                Console.WriteLine($"Incorrect income entered:{temp}");
            }
            

            tax = TaxCalculator.ComputeTaxFor(statAbv, incomeTaxEarn);
            Console.WriteLine(tax);
         
        } while (true);


    }
    }

}

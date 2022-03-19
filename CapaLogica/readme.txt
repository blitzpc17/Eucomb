Motivation:
Wanted to export collection (IEnumerator) to export with maximum 3 line of code.

Sample Application on how to use the ExcelWriter.dll

Create a Cosole Application and copy and past following 

using ExcelWriter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExcelWriterTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //CultureInfo ci_enUS = new CultureInfo("en-US");
            //CultureInfo ci_fr = new CultureInfo("fr");

            //Thread.CurrentThread.CurrentCulture = ci_enUS;
            //Thread.CurrentThread.CurrentUICulture = ci_enUS;

            //Uncomment following to see how fench resource will be returned
            //Thread.CurrentThread.CurrentCulture = ci_fr;
            //Thread.CurrentThread.CurrentUICulture = ci_fr;

            var bankAccounts = new List<Account> {
                                    new Account {
                                                  ID = 345678,
                                                  Balance = 541.27
                                                },
                                    new Account {
                                                  ID = 1230221,
                                                  Balance = -127.44
                                                }
                                };

            IExport<Account> AccountExport = new ExcelWriter<Account>();
            byte[] excelResult = AccountExport.Export(bankAccounts);
            File.WriteAllBytes(@"C:\temp\Testing.xlsx", excelResult);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
	
	public class Account
    {
        //[ExportCustom(typeof(HeaderNameResource), "header1", 1)]
        [ExportCustom("header1", 1)]
        public int ID { get; set; }

        //[ExportCustom(typeof(HeaderNameResource), "header2", 2)]
        [ExportCustom("header2", 2)]
        public double Balance { get; set; }

        public string dontExportToExcel { get; set; }
    }
	
	
}

Line One - Create an instance of AccountExport variable and pass Class Type
Line Two - Call Export Method with any collection which implements IEnumerator
Line Three - save bytes to hard disk or write it Response object for Web Application.

Run the console application and you will see the excel file saved to your temp folder (or any other folder as per your setting).





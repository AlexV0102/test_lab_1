using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClassLibrary;
using ErrorLibrary;

namespace testing_lab1
{
    [TestClass]
    public class UnitTest1
    {
        private string connectionString = @"Data Source=DESKTOP-UMRBOQ7\MSSQLSERVER01;Initial Catalog=DBLibraryCalculator;Integrated Security=True;Connect Timeout=30";


        [TestMethod]
        public void FormatFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT InputExpression, ExpectedResult FROM FormatTestCases", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string inputExpression = reader.GetString(0);
                    string expectedResult = reader.GetString(1);

                    AnalaizerClass.expression = inputExpression;


                    string result = AnalaizerClass.Format();

                    if (expectedResult.StartsWith("&Error"))
                    {
                        StringAssert.StartsWith(result, expectedResult.Split(' ')[0]);
                    }
                    else
                    {
                        Assert.AreEqual(expectedResult, result);
                    }
                }
                reader.Close();
            }
        }

    }
}

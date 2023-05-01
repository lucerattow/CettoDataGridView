using CettoDataGridView.TEST.Enums;
using CettoDataGridView.TEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CettoDataGridView.TEST.DataGenerator
{
    public static class ConsumptionDataGenerator
    {
        public static List<Consumption> GetAll()
        {
            var result = new List<Consumption>();
            DateTime startingDate;

            result.AddRange(GetSingle(2020));

            startingDate = new DateTime(2020, 05, 01);
            result.AddRange(GetInstallment(startingDate, 6, "Compre joystick", -5000));

            startingDate = new DateTime(2012, 03, 01);
            result.AddRange(GetInstallment(startingDate, 3, "Compre Tualla", -2000));

            startingDate = new DateTime(2019, 11, 01);
            result.AddRange(GetInstallment(startingDate, 12, "Compre Juego de Caserolas", -10000));

            startingDate = new DateTime(2019, 11, 01);
            result.AddRange(GetService(startingDate, 12, "Pago de luz", -2000));

            startingDate = new DateTime(2019, 11, 01);
            result.AddRange(GetService(startingDate, 12, "Pago de gas", -300));

            //Relleno los campos Id
            for (int i = 1; i <= result.Count; i++)
            {
                result[i - 1].Id = i;
            }

            return result;
        }

        private static List<Consumption> GetSingle(int year)
        {
            var result = new List<Consumption>();

            //int Id
            //ConsumptionType Type
            //DateTime Date
            //string Description
            //decimal Amount

            for (int i = 1; i <= 12; i++)
            {
                result.Add(new Consumption
                { 
                    Type = ConsumptionType.Single,
                    Date = new DateTime(year, i, 1),
                    Description = "Compra loca",
                    Amount = -1000 * i,
                });
            }

            return result;
        }

        private static List<Consumption> GetInstallment(DateTime startingDate, int Installment, string label, decimal amount)
        {
            var result = new List<Consumption>();

            for (int i = 0; i < Installment; i++)
            {
                result.Add(new Consumption
                {
                    Type = ConsumptionType.Single,
                    Date = startingDate.AddMonths(i),
                    Description = $"{label} ({i}/{Installment})",
                    Amount = amount,
                });
            }

            return result;
        }

        private static List<Consumption> GetService(DateTime startingDate, int repetitions, string label, decimal amount)
        {
            var result = new List<Consumption>();

            for (int i = 0; i < repetitions; i++)
            {
                result.Add(new Consumption
                {
                    Type = ConsumptionType.Single,
                    Date = startingDate.AddMonths(i),
                    Description = label,
                    Amount = amount,
                });
            }

            return result;
        }
    }
}

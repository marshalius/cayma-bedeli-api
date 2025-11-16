using CaymaBedeliAPI.Models;

namespace CaymaBedeliAPI.Services
{
    public class CancellationCalculator
    {
        public CancellationResult Calculate(CancellationRequest request)
        {
            decimal resultFromStart = CalculateFromStart(request.StartDate, request.Discount, request.Price) + request.ActivationFee + request.InstallationFee + request.ModemFee;
            decimal resultToEnd = CalculateToEnd(request.EndDate, request.Price);
            decimal result = Math.Min(resultFromStart, resultToEnd);


            return new CancellationResult
            {
                CancellationFee = result
            };
            
        }

        private decimal CalculateFromStart(DateTime start, decimal discount, decimal price)
        {
            DateTime today = DateTime.Today;
            int totalMonths = (today.Year - start.Year) * 12 + (today.Month - start.Month);

            int days;
            if (today.Day >= start.Day)
            {
                days = today.Day - start.Day;
            }
            else
            {
                totalMonths--;
                DateTime prevMonth = today.AddMonths(-1);
                days = (prevMonth.Day - start.Day) + today.Day;
            }

            decimal result = totalMonths * discount + days * discount / 30;

            return result;
        }

        private decimal CalculateToEnd(DateTime end, decimal price)
        {
            DateTime today = DateTime.Today;
            int totalMonths = (end.Year - today.Year) * 12 + (end.Month - today.Month);

            int days;
            if (end.Day >= today.Day)
            {
                days = end.Day - today.Day;
            }
            else
            {
                totalMonths--;
                DateTime prevMonthEnd = end.AddMonths(-1);
                days = (prevMonthEnd.Day - today.Day) + end.Day;
            }

            decimal result = totalMonths * price + days * price / 30;

            return result;
        }
    }
}

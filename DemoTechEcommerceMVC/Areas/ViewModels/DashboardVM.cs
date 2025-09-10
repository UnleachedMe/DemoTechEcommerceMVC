using System.ComponentModel.DataAnnotations;

namespace DemoTechEcommerceMVC.Areas.ModelViews
{
    public class DashboardVM
    {
        
        [Display(Name = "Gains Mensuels")]
        [DataType(DataType.Currency)]
        public double MonthlyEarnings { get; set; }

        [Display(Name = "Gains Annuels")]
        [DataType(DataType.Currency)]
        public double AnnualEarnings { get; set; }

        // Statistiques commandes
        [Display(Name = "Commandes Totales")]
        public int TotalOrders { get; set; }

        [Display(Name = "Transactions")]
        public int TotalTransactions { get; set; }

    }
}

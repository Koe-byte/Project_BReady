using System.Collections.Generic;

namespace ProjectBReadyWPF.Backend.Interfaces
{
    /// <summary>
    /// Keeps rolling snapshots so admin charts can show change over the current session.
    /// </summary>
    public interface IDashboardTrendTracker
    {
        void RecordSnapshot(
            double occupancyPercent,
            int reliefItems,
            int foodQuantity,
            int medicalQuantity,
            int evacuees,
            int availableSlots,
            int openShelters);

        IReadOnlyList<double> GetOccupancyTrend();
        IReadOnlyList<double> GetReliefTrend();
        IReadOnlyList<double> GetFoodTrend();
        IReadOnlyList<double> GetMedicalTrend();
        IReadOnlyList<double> GetEvacueeTrend();
        IReadOnlyList<double> GetAvailableBedsTrend();
        IReadOnlyList<double> GetOpenSheltersTrend();
    }
}

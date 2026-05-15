using System.Collections.Generic;
using System.Linq;
using ProjectBReadyWPF.Backend.Interfaces;

namespace ProjectBReadyWPF.Backend.Services
{
    public class DashboardTrendTracker : IDashboardTrendTracker
    {
        private const int MaxPoints = 7;

        private readonly Queue<double> _occupancy = new();
        private readonly Queue<double> _relief = new();
        private readonly Queue<double> _food = new();
        private readonly Queue<double> _medical = new();
        private readonly Queue<double> _evacuees = new();
        private readonly Queue<double> _availableBeds = new();
        private readonly Queue<double> _openShelters = new();

        public void RecordSnapshot(
            double occupancyPercent,
            int reliefItems,
            int foodQuantity,
            int medicalQuantity,
            int evacuees,
            int availableSlots,
            int openShelters)
        {
            Enqueue(_occupancy, occupancyPercent);
            Enqueue(_relief, reliefItems);
            Enqueue(_food, foodQuantity);
            Enqueue(_medical, medicalQuantity);
            Enqueue(_evacuees, evacuees);
            Enqueue(_availableBeds, availableSlots);
            Enqueue(_openShelters, openShelters);
        }

        public IReadOnlyList<double> GetOccupancyTrend() => ToSeries(_occupancy);
        public IReadOnlyList<double> GetReliefTrend() => ToSeries(_relief);
        public IReadOnlyList<double> GetFoodTrend() => ToSeries(_food);
        public IReadOnlyList<double> GetMedicalTrend() => ToSeries(_medical);
        public IReadOnlyList<double> GetEvacueeTrend() => ToSeries(_evacuees);
        public IReadOnlyList<double> GetAvailableBedsTrend() => ToSeries(_availableBeds);
        public IReadOnlyList<double> GetOpenSheltersTrend() => ToSeries(_openShelters);

        private static void Enqueue(Queue<double> queue, double value)
        {
            queue.Enqueue(value);
            while (queue.Count > MaxPoints)
                queue.Dequeue();
        }

        private static List<double> ToSeries(Queue<double> queue)
        {
            var list = queue.ToList();
            if (list.Count == 0)
                return new List<double> { 0, 0 };

            if (list.Count == 1)
                return new List<double> { list[0], list[0] };

            return list;
        }
    }
}

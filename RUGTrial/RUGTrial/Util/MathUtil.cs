using RUGTrial.Models;

namespace RUGTrial.Util
{
    public static class MathUtil
    {
        public static IEnumerable<AddressPopulationInfo> CalculateMostPopulousStates(IEnumerable<Address> addresses, int limit)
        {
            return addresses.GroupBy(a => new { a.State })
                            .Select(group => new AddressPopulationInfo
                            {
                                State = group.Key.State,
                                Count = group.Count()
                            })
                            .OrderByDescending(x => x.Count)
                            .Take(limit)
                            .ToList();
        }
    }
}

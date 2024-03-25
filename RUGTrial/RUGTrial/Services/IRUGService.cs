using RUGTrial.Models.Requests;
using RUGTrial.Models.Responses;

namespace RUGTrial.Services
{
    public interface IRUGService
    {
        /// <summary>
        /// Calculates percentages of different groups.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="RUGResponseModel"/> populated with a collection of calculated results.</returns>
        RUGResponseModel CalculatePercentages(RUGRequestModel request);
    }
}

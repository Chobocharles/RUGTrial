using RUGTrial.Constants;
using RUGTrial.Models;
using RUGTrial.Models.Requests;
using RUGTrial.Models.Responses;
using RUGTrial.Util;

namespace RUGTrial.Services
{
    public class RUGService : IRUGService
    {
        /// <inheritdoc/>
        public RUGResponseModel CalculatePercentages(RUGRequestModel request)
        {
            var calculatedResults = new List<CalculatedResult>();
            calculatedResults.AddRange(SplitFirstNames(request));
            calculatedResults.AddRange(SplitLastNames(request));
            calculatedResults.AddRange(CalculatePercentageByState(request));
            calculatedResults.AddRange(CalculatePercentageByAge(request));

            var response = new RUGResponseModel
            {
                CalculatedResults = calculatedResults
            };

            return response;
        }

        private List<CalculatedResult> CalculatePercentageByAge(RUGRequestModel request)
        {
            var firstAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.MINIMUM_AGE
                    && u.BirthDate.Age < NumericalConstants.SECOND_AGE_TIER_START);

            var calculatedResults = new List<CalculatedResult>
            {
                new()
                {
                    Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.MINIMUM_AGE} - {NumericalConstants.SECOND_AGE_TIER_START - 1}",
                    Result = firstAgeGroup.Count() / request.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstWholeList(firstAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                    FemalePercentage = CalculateGenderPercentageAgainstWholeList(firstAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
                }
            };

            var secondAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.SECOND_AGE_TIER_START
                    && u.BirthDate.Age < NumericalConstants.THIRD_AGE_TIER_START);

            calculatedResults.Add(new()
            {
                Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.SECOND_AGE_TIER_START} - {NumericalConstants.THIRD_AGE_TIER_START - 1}",
                Result = secondAgeGroup.Count() / request.Users.Count,
                MalePercentage = CalculateGenderPercentageAgainstWholeList(secondAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                FemalePercentage = CalculateGenderPercentageAgainstWholeList(secondAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
            });
                
            var thirdAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.THIRD_AGE_TIER_START 
                    && u.BirthDate.Age < NumericalConstants.FOURTH_AGE_TIER_START);
            
            calculatedResults.Add(new()
            {
                Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.THIRD_AGE_TIER_START} - {NumericalConstants.FOURTH_AGE_TIER_START - 1}",
                Result = thirdAgeGroup.Count() / request.Users.Count,
                MalePercentage = CalculateGenderPercentageAgainstWholeList(thirdAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                FemalePercentage = CalculateGenderPercentageAgainstWholeList(thirdAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
            });

            var fourthAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.FOURTH_AGE_TIER_START 
                    && u.BirthDate.Age < NumericalConstants.FIFTH_AGE_TIER_START);

            calculatedResults.Add(new()
            {
                Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.FOURTH_AGE_TIER_START} - {NumericalConstants.FIFTH_AGE_TIER_START - 1}",
                Result = fourthAgeGroup.Count() / request.Users.Count,
                MalePercentage = CalculateGenderPercentageAgainstWholeList(fourthAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                FemalePercentage = CalculateGenderPercentageAgainstWholeList(fourthAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
            });

            var fifthAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.FIFTH_AGE_TIER_START 
                    && u.BirthDate.Age < NumericalConstants.MAXIMUM_AGE);

            calculatedResults.Add(new()
            {
                Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.FIFTH_AGE_TIER_START} - {NumericalConstants.MAXIMUM_AGE - 1}",
                Result = fifthAgeGroup.Count() / request.Users.Count,
                MalePercentage = CalculateGenderPercentageAgainstWholeList(fifthAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                FemalePercentage = CalculateGenderPercentageAgainstWholeList(fifthAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
            });

            var finalAgeGroup = request.Users
                    .Where(u => u.BirthDate.Age >= NumericalConstants.MAXIMUM_AGE);

            calculatedResults.Add(new()
            {
                Rule = $"{RuleConstants.AGE_PERCENTAGE_OF_PEOPLE} - Age Group {NumericalConstants.MAXIMUM_AGE}+",
                Result = finalAgeGroup.Count() / request.Users.Count,
                MalePercentage = CalculateGenderPercentageAgainstWholeList(finalAgeGroup.Where(fg => string.Equals(AppConstants.MALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.MALE, request.Users),
                FemalePercentage = CalculateGenderPercentageAgainstWholeList(finalAgeGroup.Where(fg => string.Equals(AppConstants.FEMALE, fg.Gender, StringComparison.OrdinalIgnoreCase)), AppConstants.FEMALE, request.Users)
            });

            return calculatedResults;
        }

        private List<CalculatedResult> SplitFirstNames(RUGRequestModel randomUsers)
        {
            var aThroughMFirstNames = randomUsers.Users.Where(u => char.ToLower(u.Name.First[0]) is >= 'a' and <= 'm');
            var nThroughZFirstNames = randomUsers.Users.Where(u => char.ToLower(u.Name.First[0]) is >= 'n' and <= 'z');

            var calculatedResults = new List<CalculatedResult>
            {
                new()
                {
                    Rule = RuleConstants.FIRST_NAME_A_THROUGH_M,
                    Result = aThroughMFirstNames.Count() / randomUsers.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstList(aThroughMFirstNames, AppConstants.MALE),
                    FemalePercentage = CalculateGenderPercentageAgainstList(aThroughMFirstNames, AppConstants.FEMALE)
                },
                new()
                {
                    Rule = RuleConstants.FIRST_NAME_N_THROUGH_Z,
                    Result = nThroughZFirstNames.Count() / randomUsers.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstList(nThroughZFirstNames, AppConstants.MALE),
                    FemalePercentage = CalculateGenderPercentageAgainstList(nThroughZFirstNames, AppConstants.FEMALE)
                }
            };

            return calculatedResults;
        }

        private List<CalculatedResult> SplitLastNames(RUGRequestModel randomUsers)
        {
            var aThroughMLastNames = randomUsers.Users.Where(u => char.ToLower(u.Name.Last[0]) is >= 'a' and <= 'm');
            var nThroughZLastNames = randomUsers.Users.Where(u => char.ToLower(u.Name.Last[0]) is >= 'n' and <= 'z');

            var calculatedResults = new List<CalculatedResult>
            {
                new()
                {
                    Rule = RuleConstants.LAST_NAME_A_THROUGH_M,
                    Result = aThroughMLastNames.Count() / randomUsers.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstList(aThroughMLastNames, AppConstants.MALE),
                    FemalePercentage = CalculateGenderPercentageAgainstList(aThroughMLastNames, AppConstants.FEMALE)
                },
                new()
                {
                    Rule = RuleConstants.LAST_NAME_N_THROUGH_Z,
                    Result = nThroughZLastNames.Count() / randomUsers.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstList(nThroughZLastNames, AppConstants.MALE),
                    FemalePercentage = CalculateGenderPercentageAgainstList(nThroughZLastNames, AppConstants.FEMALE)
                }
            };

            return calculatedResults;
        }

        private static decimal CalculateGenderPercentageAgainstList(IEnumerable<User> users, string comparisonString)
        {
            var numerator = users.Count(u => string.Equals(u.Gender, comparisonString, StringComparison.OrdinalIgnoreCase));
            var denominator = users.Count();
            return denominator > 0 ? (numerator / denominator) * 100 : decimal.Zero;
        }

        private static decimal CalculateGenderPercentageAgainstWholeList(IEnumerable<User> providedUsers, string comparisonString, IEnumerable<User> completeUserList)
        {
            var numerator = providedUsers.Count(u => string.Equals(u.Gender, comparisonString, StringComparison.OrdinalIgnoreCase));
            var denominator = completeUserList.Count();
            return denominator > 0 ? (numerator / denominator) * 100 : decimal.Zero;
        }

        private List<CalculatedResult> CalculatePercentageByState(RUGRequestModel request)
        {
            var addresses = request.Users?.Select(u => u.Location);
            var mostPopulousStates = MathUtil.CalculateMostPopulousStates(addresses, 10);

            var calculatedResults = new List<CalculatedResult>();
            foreach (var state in mostPopulousStates)
            {
                var usersInState = GetUsersByState(request, state.State);

                calculatedResults.Add(new()
                {
                    Rule = $"{RuleConstants.STATE_PERCENTAGE_OF_PEOPLE}: {state.State}",
                    Result = state.Count / request.Users.Count,
                    MalePercentage = CalculateGenderPercentageAgainstWholeList(usersInState, AppConstants.MALE, request.Users),
                    FemalePercentage = CalculateGenderPercentageAgainstWholeList(usersInState, AppConstants.FEMALE, request.Users)
                });
            };

            return calculatedResults;
        }

        private IEnumerable<User> GetUsersByState(RUGRequestModel request, string State)
        {
            var recordsForState = request.Users.Where(u => string.Equals(u.Location.State, State, StringComparison.OrdinalIgnoreCase));
            return recordsForState;
        }
    }
}

using MannIsland.ModulusChecking.API.FileAccess;
using System;
using System.Collections.Generic;

namespace MannIsland.ModulusChecking.API.Services
{
    //
    // Process any Business Logic here
    //
    public class ModulusWeightService : IModulusWeightService
    {
        public ModulusWeightService()
        {
        }

        /// <summary>
        ///     validate the sort code, add weight for validation calculation.
        ///     note the notation letters per sortcode and accountNumber digit.
        /// </summary>
        /// <param name="sortcode">notation (uvwxyz)</param>
        /// <param name="accountNumber">notation (abcdefgh)</param>
        /// <returns>Y = valie : N = notValid</returns>
        public string Validate(string sortcode, string accountNumber)
        {
            // find the sortcodes that match the sortcode ranges.
            List<Models.ModulusWeight> mwList = ReadResource.FindMatched_Sortcodes(sortcode);

            // if there were no matches it should be presumed valid unless other evidence implies otherwise.
            if (mwList == null || mwList.Count == 0) return "Y";

            // loop the matched lines.
            foreach (Models.ModulusWeight mwItem in mwList)
            {
                // calculate the model values.
                if (Calculate(mwItem, sortcode, accountNumber).Equals("N")) return "N";
            }
            // it must be legit :)
            return "Y";

        }

        /// <summary>
        /// Calculate if its valid
        /// </summary>
        /// <param name="modulusweight">ModulusWeight item model</param>
        /// <param name="sortcode">notation (uvwxyz)</param>
        /// <param name="accountNumber">notation (abcdefgh)</param>
        /// <returns>Y = valie : N = notValid</returns>
        private string Calculate(Models.ModulusWeight modulusweight, string sortcode, string accountNumber)
        {

            // whats the modules check to use?
            string modulusCheck = modulusweight.AIg;

            // is the an excpetion rule to follow? rules can change the modulusCheck
            int exceptionRule = GetExceptionRule(ref modulusCheck, modulusweight.ExceptionRule, accountNumber);

            // calculate the total
            int total = GetTotal(sortcode + accountNumber, modulusweight.Weight, modulusCheck, exceptionRule);

            // divide by the ALG
            int remainder = GetRemainder(modulusCheck, total);
            
            // does it pass or fail
            if (exceptionRule == 4)
            {
                // calc the check-digit for equal to remainder value
                int checkDigit = Convert.ToInt16(accountNumber.Substring(6, 2));
                if (remainder != checkDigit)
                    return "N";
            }
            // return fail if the reaminder not zero.
            else if (remainder != 0)
                return "N";

            return "Y";
        }


        /// <summary>
        /// get the remainder value
        /// </summary>
        /// <param name="modulusCheck">modulus string value</param>
        /// <param name="total">the total</param>
        /// <returns>the reminder from the total / modulus</returns>
        private int GetRemainder(string modulusCheck, int total)
        {
            int remainder = 0;
            switch (modulusCheck)   
            {
                case "MOD10":
                case "DBLAL":
                    remainder = total % 10;
                    break;

                case "MOD11":
                    remainder = total % 11;
                    break;

            }

            return remainder;
        }


        /// <summary>
        /// Get the total from the calculations
        /// </summary>
        /// <param name="sortcodeaccountNumber">initial digit value for calc (a)</param>
        /// <param name="weight">weight list for 2nd digit for calc (b)</param>
        /// <param name="modulusCheck">modulus makes a difference for use total values or individual digits</param>
        /// <param name="exceptionRule">the exception rule can effect the calculations</param>
        /// <returns>total int value</returns>
        private int GetTotal(string sortcodeaccountNumber, List<int> weight, string modulusCheck, int exceptionRule)
        {
            int total = 0;
            int i = 0;
            // loop sortcode+accountnumber for the matches we got.
            foreach (char letter in (sortcodeaccountNumber))
            {
                bool calcRequired = true;

                // sum up the weight if the calc is required
                // exception 7 enforced sets notation values (u-b) no calculation. (zeroise).
                if (exceptionRule == 7 & i < 8) calcRequired = false;

                // spec says DBLAL calculate from the right using 1,2 alternate, then states just use weights list.
                // so will multiply as usual. TODO: Need to clarify
                if (calcRequired) total += GetDigitValue(modulusCheck, Convert.ToInt16(letter) * weight[i]);
                i++;
            }

            return total;
        }


        /// <summary>
        /// Get the exceptionRule if one available and passes business logic
        /// </summary>
        /// <param name="modulusCheck">moduluscheck value</param>
        /// <param name="exceptionRule">exception rule</param>
        /// <param name="accountNumber">account number</param>
        /// <returns>exceptionRule</returns>

        private int GetExceptionRule(ref string modulusCheck, int exceptionRule, string accountNumber)
        {
            if (exceptionRule == 0) return 0;

            // exception 4 forces the MOD11 to be used.
            if (exceptionRule == 4)
                modulusCheck = "MOD11";

            // exception 7 and accountnumber notation value (g) then enforce exception 7.
            else if (exceptionRule == 7)
            {
                if (Convert.ToInt16(accountNumber.Substring(6, 1)) != 9) exceptionRule = 0;
            }
            else 
            {
                // were not validating any other exception rules
                // feed the exceptionRule back to the API.
            }

            return exceptionRule;
        }


        /// <summary>
        /// check if digits sum or just value
        /// </summary>
        /// <param name="modulusCheck">DBLAL = individual digit sum</param>
        /// <param name="value">initial value</param>
        /// <returns>calculation or value</returns>
        private int GetDigitValue(string modulusCheck, int value)
        {
            if (modulusCheck.Equals("DBLAL"))
            {
                //Add the individual digit numbers together
                int total = 0;
                foreach (char s in value.ToString())
                {
                    total += Convert.ToInt16(s);
                }

                return total;
            }
            else
                return value;
        }
    }

}
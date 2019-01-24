using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MannIsland.ModulusChecking.API.Models
{
    //
    // Modulus weight structure Model
    //
    public class ModulusWeight
    {
        /// <summary>
        /// The sortcode start range
        /// </summary>
        [MaxLength(6)]
        public string SortcodeStart { get; set; }

        /// <summary>
        /// The sortcode end range
        /// </summary>
        [MaxLength(6)]
        public string SortcodeEnd { get; set; }

        /// <summary>
        /// The 
        /// </summary>
        [MaxLength(5)]
        public string AIg { get; set; }

        /// <summary>
        /// The weights to multiply
        /// </summary>
        public List<int> Weight { get; set; }

        /// <summary>
        /// The exception code for furthur logic
        /// </summary>
        public int ExceptionRule { get; set; }
    }
}
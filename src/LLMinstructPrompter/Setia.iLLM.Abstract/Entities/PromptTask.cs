using System;
using System.Collections.Generic;
using System.Text;

namespace Setia.iLLM.Abstract.Entities
{
    public class PromptTask
    {
        /// <summary>
        /// Default Value 1, defines the order of execution
        /// </summary>
        public int Order { get; set; } = 1;
        
        /// <summary>
        /// Task that must be performed
        /// </summary>
        public string Task { get; set; }
    }
}

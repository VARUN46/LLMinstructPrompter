using LLMinstructPrompter.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LLMinstructPrompter.Abstract.Prompts
{
    public interface IPromptDesigns
    {
        /// <summary>
        /// Define the single task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        IPromptDesigns SetTask(PromptTask task);
        
        /// <summary>
        /// Define the tasks in order you want to perform
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        IPromptDesigns SetTasks(params PromptTask[] tasks);

        IPromptDesigns SetOutputFormat(OutputType outputType);

        IPromptDesigns SetOutputUniqueness(OutputRandomness outputRandomness);

        PromptableObject GetPrompt();

    }
}

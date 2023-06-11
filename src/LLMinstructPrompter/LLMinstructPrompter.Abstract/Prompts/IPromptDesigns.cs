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
        
        /// <summary>
        ///Set the output from specified output types 
        /// </summary>
        /// <param name="outputType"></param>
        /// <returns></returns>
        IPromptDesigns SetOutputFormat(OutputType outputType);

        /// <summary>
        /// Set output uniqueness from given options
        /// </summary>
        /// <param name="outputRandomness"></param>
        /// <returns></returns>
        IPromptDesigns SetOutputUniqueness(OutputRandomness outputRandomness);

        /// <summary>
        /// Method used to get prompt that can be ingested to LLM model
        /// </summary>
        /// <returns></returns>
        PromptableObject GetPrompt();

        /// <summary>
        /// Set the output object type for applicable formats like JSON, CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPromptDesigns OutputObject<T>();

    }
}

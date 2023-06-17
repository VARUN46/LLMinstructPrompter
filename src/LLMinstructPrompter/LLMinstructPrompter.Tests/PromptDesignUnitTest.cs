using Setia.iLLM.Abstract.Prompts;
using Setia.iLLM.Prompts;
using Setia.iLLM.Tests.TestEntities;

namespace Setia.iLLM.Tests
{
    public class PromptDesignUnitTest
    {
        IPromptDesigns promptDesigns;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SingleTaskRawOutputTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get sentiment from text 'I am happy'" });
            promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.Raw);
            var prompt = promptDesigns.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test]
        public void SingleTaskJsonOutputTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get sentiment from text 'I am happy'" });
            promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
            promptDesigns.OutputObject<SentimentTest>();
            var prompt = promptDesigns.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test]
        public void SingleTaskCsvOutputTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get words and count from text 'Coz I am happy happy happy, get along with me coz I am happy !!'" });
            promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.CSV);
            promptDesigns.OutputObject<WordCount>();
            var prompt = promptDesigns.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test]
        public void SingleTaskJsonListOutputTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get words and count from text 'Coz I am happy happy happy, get along with me coz I am happy !!'" });
            promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
            promptDesigns.OutputObject<List<WordCount>>();
            var prompt = promptDesigns.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test]
        public void MultipleTasksJsonListOutputTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTasks(
                new Abstract.Entities.PromptTask { Task = "Write the sad version of 'Coz I am happy happy happy, get along with me coz I am happy !!'" },
                new Abstract.Entities.PromptTask { Task = "Finally, Extract the count of each word", Order = 2 
                });
            promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
            promptDesigns.OutputObject<List<WordCount>>();
            var prompt = promptDesigns.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test]
        public void NoSystemPromptTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Write a poem" }).DiscardSystemPrompt();
            var prompt = promptBuilder.GetPrompt();
            Assert.IsNotNull(prompt);
        }

        [Test] 
        public void TemplateReplacementTest()
        {
            promptDesigns = new PromptDesigns();
            var promptBuilder = promptDesigns.SetTasks(new Abstract.Entities.PromptTask { Task = "Rephrase the given text as a song: {text}" }, new Abstract.Entities.PromptTask { Task = "Finally, Map each keyword to category accurately and generate output\nCategories:\n{categories}", Order = 2 })
                                              .ReplaceTemplateWithText(new Abstract.Entities.TemplateTextPair { TemplateVariableName = "{categories}",Value = "Vehicle, Names"}, new Abstract.Entities.TemplateTextPair { TemplateVariableName = "{text}", Value = "Varun must own a hummer so he can have a good ride!!" })
                                              .SetOutputFormat(Abstract.Entities.OutputType.JSON)
                                              .OutputObject<List<KeywordCategory>>()
                                              .SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
            var prompt = promptBuilder.GetPrompt();
            Assert.IsNotNull(prompt);

        }
    }
}
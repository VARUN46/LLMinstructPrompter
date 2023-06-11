using LLMinstructPrompter.Abstract.Prompts;
using LLMinstructPrompter.Prompts;
using LLMinstructPrompter.Tests.TestEntities;

namespace LLMinstructPrompter.Tests
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
    }
}
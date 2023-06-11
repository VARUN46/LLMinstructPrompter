<!DOCTYPE html>
<html>
<head>
  <meta charset="UTF-8">
  <title>Code README</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.5;
    }
    
    h1 {
      margin-top: 0;
    }
    
    h2 {
      margin-bottom: 0.5em;
    }
    
    pre {
      background-color: #f4f4f4;
      padding: 10px;
      overflow-x: auto;
    }
    
    code {
      font-family: Consolas, monospace;
    }
  </style>
</head>
<body>
  <h1>Code README</h1>
  <p>This package is contributed by Varun Setia</p>
  <h2>Table of Contents</h2>
  <ul>
    <li><a href="#singletaskrawoutputtest">SingleTaskRawOutputTest</a></li>
    <li><a href="#singletaskjsonoutputtest">SingleTaskJsonOutputTest</a></li>
    <li><a href="#singletaskcsvoutputtest">SingleTaskCsvOutputTest</a></li>
    <li><a href="#singletaskjsonlistoutputtest">SingleTaskJsonListOutputTest</a></li>
    <li><a href="#multipletasksjsonlistoutputtest">MultipleTasksJsonListOutputTest</a></li>
    <li><a href="#nosystemprompttest">NoSystemPromptTest</a></li>
  </ul>

  <hr>

  <h2 id="singletaskrawoutputtest">SingleTaskRawOutputTest</h2>
  <pre><code>public void SingleTaskRawOutputTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get sentiment from text 'I am happy'" });
    promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
    promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.Raw);
    var prompt = promptDesigns.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

  <h2 id="singletaskjsonoutputtest">SingleTaskJsonOutputTest</h2>
  <pre><code>public void SingleTaskJsonOutputTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get sentiment from text 'I am happy'" });
    promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
    promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
    promptDesigns.OutputObject&lt;SentimentTest&gt;();
    var prompt = promptDesigns.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

  <h2 id="singletaskcsvoutputtest">SingleTaskCsvOutputTest</h2>
  <pre><code>public void SingleTaskCsvOutputTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get words and count from text 'Coz I am happy happy happy, get along with me coz I am happy !!'" });
    promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
    promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.CSV);
    promptDesigns.OutputObject&lt;WordCount&gt;();
    var prompt = promptDesigns.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

  <h2 id="singletaskjsonlistoutputtest">SingleTaskJsonListOutputTest</h2>
  <pre><code>public void SingleTaskJsonListOutputTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Get words and count from text 'Coz I am happy happy happy, get along with me coz I am happy !!'" });
    promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
    promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
    promptDesigns.OutputObject&lt;List&lt;WordCount&gt;&gt;();
    var prompt = promptDesigns.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

  <h2 id="multipletasksjsonlistoutputtest">MultipleTasksJsonListOutputTest</h2>
  <pre><code>public void MultipleTasksJsonListOutputTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTasks(
        new Abstract.Entities.PromptTask { Task = "Write the sad version of 'Coz I am happy happy happy, get along with me coz I am happy !!'" },
        new Abstract.Entities.PromptTask { Task = "Finally, Extract the count of each word", Order = 2 
        });
    promptBuilder.SetOutputUniqueness(Abstract.Entities.OutputRandomness.Low);
    promptDesigns.SetOutputFormat(Abstract.Entities.OutputType.JSON);
    promptDesigns.OutputObject&lt;List&lt;WordCount&gt;&gt;();
    var prompt = promptDesigns.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

  <h2 id="nosystemprompttest">NoSystemPromptTest</h2>
  <pre><code>public void NoSystemPromptTest()
{
    promptDesigns = new PromptDesigns();
    var promptBuilder = promptDesigns.SetTask(new Abstract.Entities.PromptTask { Task = "Write a poem" }).DiscardSystemPrompt();
    var prompt = promptBuilder.GetPrompt();
    Assert.IsNotNull(prompt);
}</code></pre>

</body>
</html>

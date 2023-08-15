using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;
using EvolutionSim;
public class GeneTest : Component
{
    readonly string dnaString = "12303456032";
    public override void Start()
    {
        Console.WriteLine("Start");
        string[] genes = dnaString.Split('0');
        foreach (string gene in genes)
        {
        BuildStep(gene);
        }
    }
    public override void Update()
    {
        
    }
    void BuildStep(string bricks)
    {
        // Replace this with actual instructions for building a house using bricks.
        // For this example, we will just print the number of bricks needed for each step.
        Console.WriteLine($"Build step: {bricks} bricks required.");
    }
}
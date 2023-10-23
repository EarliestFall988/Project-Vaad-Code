using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The main player progression class
/// </summary>
public class PlayerProgression
{
    public static PlayerProgression main;

    //just a funny name
    public string PlayerName = "Player McPlayerface";

    public Backpack PlayerBackpack = new Backpack();

    public PlayerProgression()
    {

        if (main != null)
        {
            Debug.LogError("PlayerProgression already exists");
        }
        else
            main = this;
    }

    public int Level = 1;

    public int Experience = 0;

    public int ExperienceToNextLevel = 100;


    public void AddExperience(int amount)
    {
        Experience += amount;
        if (Experience >= ExperienceToNextLevel)
        {
            Level++;
            Experience -= ExperienceToNextLevel;
            ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5f);
        }
    }
}

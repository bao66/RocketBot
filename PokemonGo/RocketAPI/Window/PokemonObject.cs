﻿using System.Collections.Generic;
using AllEnum;
using PokemonGo.RocketAPI.GeneratedCode;

namespace PokemonGo.RocketAPI.Window
{
    public class PokemonObject
    {
        private static bool initialized;
        public static Dictionary<PokemonId, int> candyToEvolveDict = new Dictionary<PokemonId, int>();

        public PokemonObject(PokemonData pokemonData)
        {
            PokemonData = pokemonData;
        }

        public PokemonData PokemonData { get; }

        public ulong Id
        {
            get { return PokemonData.Id; }
        }

        public PokemonId PokemonId
        {
            get { return PokemonData.PokemonId; }
        }

        public int Cp
        {
            get { return PokemonData.Cp; }
        }

        public int IndividualAttack
        {
            get { return PokemonData.IndividualAttack; }
        }

        public int IndividualDefense
        {
            get { return PokemonData.IndividualDefense; }
        }

        public int IndividualStamina
        {
            get { return PokemonData.IndividualStamina; }
        }

        public float GetIV
        {
            get { return PokemonData.GetIV(); }
        }

        public int Candy { get; set; } = 0;

        public int CandyToEvolve
        {
            get
            {
                if (candyToEvolveDict.ContainsKey(PokemonData.PokemonId))
                {
                    return candyToEvolveDict[PokemonData.PokemonId];
                }
                return 0;
            }
        }

        public int EvolveTimes
        {
            get
            {
                if (CandyToEvolve > 0)
                {
                    return Candy/CandyToEvolve;
                }
                return 0;
            }
        }

        public bool CanEvolve
        {
            get { return EvolveTimes > 0; }
        }

        public static async void Initilize(Client c)
        {
            if (!initialized)
            {
                var itemtemplates = await c.GetItemTemplates();
                foreach (var t in itemtemplates.ItemTemplates)
                {
                    if (t != null)
                    {
                        if (t.PokemonSettings != null)
                        {
                            candyToEvolveDict.Add(t.PokemonSettings.PokemonId, t.PokemonSettings.CandyToEvolve);
                        }
                    }
                }
                initialized = true;
            }
        }
    }
}
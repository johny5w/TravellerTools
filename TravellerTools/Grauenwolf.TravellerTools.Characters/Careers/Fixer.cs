﻿namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Fixer : Prisoner
    {
        public Fixer() : base("Fixer") { }

        protected override string AdvancementAttribute
        {
            get { return "End"; }
        }

        protected override int AdvancementTarget
        {
            get { return 5; }
        }

        protected override string SurvivalAttribute
        {
            get { return "Int"; }
        }

        protected override int SurvivalTarget
        {
            get { return 9; }
        }

        protected override void AssignmentSkills(Character character, Dice dice, int roll, bool level0)
        {
            switch (roll)
            {
                case 1:
                    character.Skills.Increase("Investigate");
                    return;
                case 2:
                    character.Skills.Increase("Broker");
                    return;
                case 3:
                    character.Skills.Increase("Deception");
                    return;
                case 4:
                    character.Skills.Increase("Streetwise");
                    return;
                case 5:
                    character.Skills.Increase("Stealth");
                    return;
                case 6:
                    character.Skills.Increase("Admin");
                    return;
            }
        }
    }
}

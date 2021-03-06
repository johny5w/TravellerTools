﻿
namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Barbarian : Drifter
    {
        public Barbarian(Book book) : base("Barbarian", book) { }

        protected override string AdvancementAttribute => "Str";

        protected override int AdvancementTarget => 7;

        protected override string SurvivalAttribute => "End";

        protected override int SurvivalTarget => 7;

        internal override void BasicTrainingSkills(Character character, Dice dice, bool all)
        {
            var roll = dice.D(6);

            if (all || roll == 1)
                character.Skills.AddRange(SpecialtiesFor("Animals"));
            if (all || roll == 2)
                character.Skills.Add("Carouse");
            if (all || roll == 3)
                character.Skills.Add("Melee", "Blade");
            if (all || roll == 4)
                character.Skills.Add("Stealth");
            if (all || roll == 5)
            {
                character.Skills.Add(new SkillTemplate("Seafarer", "Personal"));
                character.Skills.Add(new SkillTemplate("Seafarer", "Sails"));
            }
            if (all || roll == 6)
                character.Skills.Add("Survival");
        }

        internal override void AssignmentSkills(Character character, Dice dice)
        {
            switch (dice.D(6))
            {
                case 1:
                    character.Skills.Increase(dice.Choose(SpecialtiesFor("Animals")));
                    return;
                case 2:
                    character.Skills.Increase("Carouse");
                    return;
                case 3:
                    character.Skills.Increase("Melee", "Blade");
                    return;
                case 4:
                    character.Skills.Increase("Stealth");
                    return;
                case 5:
                    {
                        var skillList = new SkillTemplateCollection() { new SkillTemplate("Seafarer", "Personal"), new SkillTemplate("Seafarer", "Sails") };
                        character.Skills.Increase(dice.Choose(skillList));
                    }
                    return;
                case 6:
                    character.Skills.Increase("Survival");
                    return;
            }
        }

        internal override void TitleTable(Character character, CareerHistory careerHistory, Dice dice)
        {
            switch (careerHistory.Rank)
            {
                case 1:
                    character.Skills.Add("Survival", 1);
                    return;
                case 2:
                    careerHistory.Title = "Warrior";
                    character.Skills.Add("Melee", "Blade", 1);
                    return;
                case 3:
                    return;
                case 4:
                    careerHistory.Title = "Chieftain";
                    character.Skills.Add("Leadership", 1);
                    return;
                case 5:
                    return;
                case 6:
                    careerHistory.Title = "Warlord";
                    return;
            }
        }
    }
}


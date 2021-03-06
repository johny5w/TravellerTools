using System.Linq;

namespace Grauenwolf.TravellerTools.Characters.Careers
{
    class Retired : Career
    {
        public Retired(Book book) : base("Retired", null, book)
        {

        }

        public void Event(Character character, Dice dice)
        {
            switch (dice.D(2, 6))
            {
                case 2:
                    Injury(character, dice);
                    return;
                case 3:
                    character.AddHistory("Birth or Death involving a family member or close friend.");
                    return;
                case 4:
                    character.AddHistory("A romantic relationship ends badly. Gain a Rival or Enemy.");
                    return;
                case 5:
                    character.AddHistory("A romantic relationship deepens, possibly leading to marriage. Gain an Ally.");
                    return;
                case 6:
                    character.AddHistory("A new romantic starts. Gain an Ally.");
                    return;
                case 7:
                    character.AddHistory("Gained a contact.");
                    return;
                case 8:
                    character.AddHistory("Betrayal. Convert an Ally into a Rival or Enemy.");
                    return;
                case 9:
                    character.AddHistory("Moved to a new world.");
                    character.NextTermBenefits.QualificationDM += 1;
                    return;
                case 10:
                    character.AddHistory("Good fortune");
                    character.BenefitRollDMs.Add(2);
                    return;
                case 11:
                    if (dice.D(2) == 1)
                    {
                        character.BenefitRolls -= 1;
                        character.AddHistory("Victim of a crime");
                    }
                    else
                    {
                        character.AddHistory("Accused of a crime");
                        character.NextTermBenefits.MustEnroll = "Prisoner";
                    }
                    return;
                case 12:
                    UnusualLifeEvent(character, dice);
                    return;
            }
        }

        internal override bool Qualify(Character character, Dice dice)
        {
            return character.LongTermBenefits.Retired;
        }

        internal override void Run(Character character, Dice dice)
        {

            CareerHistory careerHistory;
            if (!character.CareerHistory.Any(pc => pc.Name == "Retired"))
            {
                character.AddHistory($"Retired at age {character.Age}");
                careerHistory = new CareerHistory("Retired", null, 0);
                character.CareerHistory.Add(careerHistory);
            }
            else
            {
                careerHistory = character.CareerHistory.Single(pc => pc.Name == "Retired");
            }
            careerHistory.Terms += 1;

            Event(character, dice);

            character.LastCareer = careerHistory;
            character.Age += 4;
            character.NextTermBenefits.MustEnroll = "Retired";


        }
    }
}

using System;
using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Maps
{
    public class World
    {
        private string m_Remarks;
        readonly HashSet<string> m_RemarksList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<string, string> s_RemarkMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static World()
        {
            s_RemarkMap.Add("As", "Asteroid Belt.");
            s_RemarkMap.Add("De", "Desert.");
            s_RemarkMap.Add("Fl", "Fluid Hydrographics (in place of water).");
            s_RemarkMap.Add("Ga", "Garden World.");
            s_RemarkMap.Add("He", "Hellworld.");
            s_RemarkMap.Add("Ic", "Ice Capped.");
            s_RemarkMap.Add("Oc", "Ocean World.");
            s_RemarkMap.Add("Va", "Vacuum World.");
            s_RemarkMap.Add("Wa", "Water World.");
            s_RemarkMap.Add("Di", "Dieback.");
            s_RemarkMap.Add("Ba", "Barren.");
            s_RemarkMap.Add("Lo", "Low Population.");
            s_RemarkMap.Add("Ni", "Non-Industrial.");
            s_RemarkMap.Add("Ph", "Pre-High Population.");
            s_RemarkMap.Add("Hi", "High Population.");
            s_RemarkMap.Add("Pa", "Pre-Agricultural.");
            s_RemarkMap.Add("Ag", "Agricultural.");
            s_RemarkMap.Add("Na", "Non-Agricultural.");
            s_RemarkMap.Add("Pi", "Pre-Industrial.");
            s_RemarkMap.Add("In", "Industrialized.");
            s_RemarkMap.Add("Po", "Poor.");
            s_RemarkMap.Add("Pr", "Pre-Rich.");
            s_RemarkMap.Add("Ri", "Rich.");
            s_RemarkMap.Add("Fr", "Frozen.");
            s_RemarkMap.Add("Ho", "Hot.");
            s_RemarkMap.Add("Co", "Cold.");
            s_RemarkMap.Add("Lk", "Locked.");
            s_RemarkMap.Add("Tr", "Tropic.");
            s_RemarkMap.Add("Tu", "Tundra.");
            s_RemarkMap.Add("Tz", "Twilight Zone.");
            s_RemarkMap.Add("Fa", "Farming.");
            s_RemarkMap.Add("Mi", "Mining.");
            s_RemarkMap.Add("Mr", "Military Rule.");
            s_RemarkMap.Add("Px", "Prison, Exile Camp.");
            s_RemarkMap.Add("Pe", "Penal Colony.");
            s_RemarkMap.Add("Re", "Reserve.");
            s_RemarkMap.Add("Sa", "Satellite.");
            s_RemarkMap.Add("Fo", "Forbidden.");
            s_RemarkMap.Add("Pz", "Puzzle.");
            s_RemarkMap.Add("Da", "Danger");
            s_RemarkMap.Add("Ab", "Data Repository.");
            s_RemarkMap.Add("An", "Ancient Site.");
            s_RemarkMap.Add("Rs", "Research Station.");
            s_RemarkMap.Add("RsA", "Research Station Alpha.");
            s_RemarkMap.Add("RsB", "Research Station Beta.");
            s_RemarkMap.Add("RsG", "Research Station Gamma.");
            s_RemarkMap.Add("Lt", "Low Technology.");
            s_RemarkMap.Add("Ht", "High Technology.");
            //s_RemarkMap.Add("Fa", "Fascinating.");
            s_RemarkMap.Add("St", "Steppeworld.");
            s_RemarkMap.Add("Ex", "Exile Camp.");
            //s_RemarkMap.Add("Pr", "Prison World.");
            s_RemarkMap.Add("Xb", "Xboat Station.");
        }


        public string Name { get; set; }
        public string Hex { get; set; }
        public string UWP { get; set; }
        public string PBG { get; set; }
        public string Zone { get; set; }
        public string Bases { get; set; }
        public string Allegiance { get; set; }
        public string Stellar { get; set; }
        public string SS { get; set; }
        public string Ix { get; set; }
        public string Ex { get; set; }
        public string Cx { get; set; }
        public string Nobility { get; set; }
        public int Worlds { get; set; }
        public int ResourceUnits { get; set; }
        public int Subsector { get; set; }
        public int Quadrant { get; set; }
        public string Remarks
        {
            get
            {
                return m_Remarks;
            }
            set
            {
                m_Remarks = value;
                m_RemarksList.Clear();
                foreach (var item in m_Remarks.Split(' '))
                    m_RemarksList.Add(item);
            }
        }

        public string LegacyBaseCode { get; set; }
        public string Sector { get; set; }
        public string SubsectorName { get; set; }
        public string AllegianceName { get; set; }

        public int JumpDistance { get; set; }

        public EHex StarportCode { get { return UWP[0]; } }
        public EHex SizeCode { get { return UWP[1]; } }
        public EHex AtmosphereCode { get { return UWP[2]; } }
        public EHex HydrographicsCode { get { return UWP[3]; } }
        public EHex PopulationCode { get { return UWP[4]; } }
        public EHex GovernmentCode { get { return UWP[5]; } }
        public EHex LawCode { get { return UWP[6]; } }
        public EHex TechCode { get { return UWP[8]; } }


        public string StarportDescription => Tables.StarportDescription(StarportCode);
        public string Starport => Tables.Starport(StarportCode);



        public int SizeKM
        {
            get
            {
                switch (SizeCode.ToString())
                {

                    case "0": return 800;
                    case "1": return 1600;
                    case "2": return 3200;
                    case "3": return 4800;
                    case "4": return 6400;
                    case "5": return 8000;
                    case "6": return 9600;
                    case "7": return 11200;
                    case "8": return 12800;
                    case "9": return 14400;
                    case "A": return 16000;
                    default: return 0;
                }
            }
        }

        /// <summary>
        /// Transits the time jump point.
        /// </summary>
        /// <param name="thrustRating">The thrust rating.</param>
        /// <param name="jumpDistanceFactor">The jump distance factor. Normally this is 100.</param>
        /// <returns>TimeSpan.</returns>
        public TimeSpan TransitTimeJumpPoint(int thrustRating, int jumpDistanceFactor = 100)
        {
            const double G = 9.80665; //meters per second per second
            var distanceM = (double)SizeKM * 1000 * jumpDistanceFactor;
            var timeSeconds = 2 * Math.Sqrt(distanceM / (G * thrustRating));
            return TimeSpan.FromSeconds(timeSeconds);
        }

        public int JumpDistanceKM(int jumpDistanceFactor = 100)
        {
            return SizeKM * jumpDistanceFactor;
        }

        public string Atmosphere
        {
            get
            {
                switch (AtmosphereCode.ToString())
                {
                    //star ports
                    case "0": return "No atmosphere.";
                    case "1": return "Trace.";
                    case "2": return "Very thin. Tainted.";
                    case "3": return "Very thin.";
                    case "4": return "Thin. Tainted.";
                    case "5": return "Thin. Breathable.";
                    case "6": return "Standard. Breathable.";
                    case "7": return "Standard. Tainted.";
                    case "8": return "Dense. Breathable";
                    case "9": return "Dense. Tainted.";
                    case "A": return "Exotic.";
                    case "B": return "Corrosive.";
                    case "C": return "Insidious.";
                    case "D": return "Dense, high.";
                    case "E": return "Ellipsoid.";
                    case "F": return "Thin, low.";
                    default: return "";
                }
            }
        }

        public string AtmosphereDescription
        {
            get
            {
                switch (AtmosphereCode.ToString())
                {
                    //star ports
                    case "0": return "No atmosphere. Requires vacc suit.";
                    case "1": return "Trace. Requires vacc suit.";
                    case "2": return "Very thin. Tainted. Requires combination respirator/filter.";
                    case "3": return "Very thin. Requires respirator.";
                    case "4": return "Thin. Tainted. Requires filter mask.";
                    case "5": return "Thin. Breathable. ";
                    case "6": return "Standard. Breathable.";
                    case "7": return "Standard. Tainted. Requires filter mask.";
                    case "8": return "Dense. Breathable";
                    case "9": return "Dense. Tainted. Requires filter mask.";
                    case "A": return "Exotic. Requires special protective equipment.";
                    case "B": return "Corrosive. Requires protective suit.";
                    case "C": return "Insidious. Requires protective suit.";
                    case "D": return "Dense, high. Breathable above a minimum altitude.";
                    case "E": return "Ellipsoid. Breathable at certain latitudes.";
                    case "F": return "Thin, low. Breathable below certain altitudes.";
                    default: return "";
                }
            }
        }
        public string Hydrographics
        {
            get
            {
                switch (HydrographicsCode.ToString())
                {
                    //star ports
                    case "0": return "No water. Desert World. ";
                    case "1": return "10% water.";
                    case "2": return "20% water.";
                    case "3": return "30% water.";
                    case "4": return "40% water.";
                    case "5": return "50% water.";
                    case "6": return "60% water.";
                    case "7": return "70% water. Equivalent to Terra or Vland.";
                    case "8": return "80% water.";
                    case "9": return "90% water.";
                    case "A": return "100% water. Water World.";
                    default: return "";
                }
            }
        }

        public double PopulationExponent
        {
            get { return Math.Pow(10, PopulationCode.Value); }
        }

        public int PopulationMultiplier
        {
            get
            {
                if (!string.IsNullOrEmpty(PBG))
                {
                    var temp = PBG.Substring(0, 1);
                    int result;
                    if (int.TryParse(temp, out result))
                        return result;
                }
                return 1;
            }
        }

        public double Population
        {
            get { return PopulationMultiplier * PopulationExponent; }
        }

        public string GovernmentType
        {
            get
            {
                switch (GovernmentCode.ToString())
                {
                    case "0": return "No Government Structure.";
                    case "1": return "Company/Corporation.";
                    case "2": return "Participating Democracy.";
                    case "3": return "Self-Perpetuating Oligarchy.";
                    case "4": return "Representative Democracy.";
                    case "5": return "Feudal Technocracy.";
                    case "6": return "Captive Government / Colony.";
                    case "7": return "Balkanization.";
                    case "8": return "Civil Service Bureaucracy.";
                    case "9": return "Impersonal Bureaucracy.";
                    case "A": return "Charismatic Dictator.";
                    case "B": return "Non-Charismatic Dictator.";
                    case "C": return "Charismatic Oligarchy.";
                    case "D": return "Religious Dictatorship.";
                    case "E": return "Religious Autocracy.";
                    case "F": return "Totalitarian Oligarchy.";
                    case "G": return "Small Station or Facility. Aslan.";
                    case "H": return "Split Clan Control. Aslan.";
                    case "J": return "Single On-world Clan Control. Aslan.";
                    case "K": return "Single Multi-world Clan Control. Aslan.";
                    case "L": return "Major Clan Control. Aslan.";
                    case "M": return "Vassal Clan Control. Aslan.";
                    case "N": return "Major Vassal Clan Control. Aslan.";
                    case "P": return "Small Station or Facility. K�kree.";
                    case "Q": return "Krurruna or Krumanak Rule for Off-world Steppelord. K�kree.";
                    case "R": return "Steppelord On-world Rule. K�kree.";
                    case "S": return "Sept. Hiver.";
                    case "T": return "Unsupervised Anarchy. Hiver.";
                    case "U": return "Supervised Anarchy. Hiver.";
                    case "W": return "Committee. Hiver.";
                    case "X": return "Droyne Hierarchy. Droyne.";
                    default: return "";
                }
            }
        }


        public string LawLevel => Tables.LawLevel(LawCode);

        public string TechLevel => Tables.TechLevel(TechCode);




        public string HexX { get { return Hex.Substring(0, 2); } }
        public string HexY { get { return Hex.Substring(2, 2); } }


        //These are added later
        public int SectorX { get; set; }
        public int SectorY { get; set; }

        public string RemarksDescription
        {
            get
            {
                var source = Remarks.Split(' ');
                var result = new List<string>();
                foreach (var remark in source)
                {
                    string des;
                    if (s_RemarkMap.TryGetValue(remark, out des))
                        result.Add(string.Format("{0}: {1}", remark, des));
                    else
                        result.Add(remark);
                }
                return string.Join(" ", result);
            }
        }

        /// <summary>
        /// Determines whether world contains specified remark. This includes zones A and R.
        /// </summary>
        /// <param name="remark">The case insensitive remark.</param>
        /// <returns><c>true</c> if the specified remark contains remark; otherwise, <c>false</c>.</returns>
        public bool ContainsRemark(string remark)
        {
            return m_RemarksList.Contains(remark) || string.Compare(Zone, remark, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public string SubSectorIndex { get; set; }

        public HashSet<string> RemarksList => m_RemarksList;
    }
}

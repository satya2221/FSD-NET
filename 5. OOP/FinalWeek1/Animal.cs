using ZooStaffNamespace;
namespace AnimalNamespace
{
    public abstract class Animal
    {
        public int Id { get; set; }
        public string? Name {get;set;}
        public string? Habitat {get;set;}
        public Animal (int id, string name, string habitat){
            Id = id;
            Name = name;
            Habitat = habitat;
        }
        private ZooStaff? _careTaker;
        public ZooStaff? CareTaker{
            get => _careTaker;
            set{
                if (value == null || value.Role == "AnimalSpecialist")
                {
                    _careTaker = value;
                }
                else
                {
                    throw new ArgumentException("Only Zoo Staff with the role 'AnimalSpecialist' can be assigned to CareTaker.");
                }
            }
        }
        public abstract string HowToMove();
        public virtual string HowToCommunicate(){
            return "";
        }
    }

    public class Crocodile : Animal
    {
        public int JumlahKaki {get;} = 4;

        public Crocodile (int Id, string Name, string Habitat):base(Id, Name, Habitat){
        }
        public override string HowToMove()
        {
            return "Crawling";
        }
    }

    public class Tiger : Animal
    {
        public Tiger(int id, string name, string habitat) : base(id, name, habitat)
        {
        }

        public int JumlahKaki {get;} = 4;
        public override string HowToMove()
        {
            return "Sprinting";
        }
        public override string HowToCommunicate(){
            return "Roaring";
        }
    }
    public class Cassowaries : Animal
    {
        public Cassowaries(int id, string name, string habitat) : base(id, name, habitat)
        {
        }

        public override string HowToMove()
        {
            return "Running";
        }
        public override string HowToCommunicate(){
            return "Booming Noises";
        }
        public string Attack(){
            return "kicking";
        }
    }
}
using AnimalNamespace;
using ZooStaffNamespace;

#region functionExtra
void PrintAnimalDetail(Animal animalDetail){
    Console.WriteLine("===Detail Hewan===");
    Console.WriteLine($"Id hewan\t: {animalDetail.Id}");
    Console.WriteLine($"Nama panggilan\t: {animalDetail.Name}");
    Console.WriteLine($"Habitat Tinggal\t: {animalDetail.Habitat}");
    Console.WriteLine($"Jenis Hewan\t:{animalDetail.GetType().Name}");
    Console.WriteLine($"Bergerak dengan cara\t: {animalDetail.HowToMove()}");
    Console.WriteLine($"Berkomunikasi dengan cara\t: {animalDetail.HowToCommunicate()}");

    // Handler jika ada yang memiliki method tersendiri sementara hanya kasuari yang punya method tersendiri
    switch(animalDetail.GetType().Name)
    {
        case "Cassowaries":
            if (animalDetail is Cassowaries cassowary)
            {
                Console.WriteLine($"Hati-hati!!! dapat menyerang dengan cara {cassowary.Attack()}");
            }
        break;
    }
    Console.WriteLine($"\tCaretaker {animalDetail.Name} si {animalDetail.GetType().Name}");
    if (animalDetail.CareTaker == null ){
        Console.WriteLine("\tBelum memiliki caretaker");
    }
    else
    {
        Console.WriteLine("\t======");
        Console.WriteLine($"\tId staff\t: {animalDetail.CareTaker.Id}");
        Console.WriteLine($"\tNama staff\t: {animalDetail.CareTaker.Name}");
        Console.WriteLine();
    }
    
}
void PrintStaffDetail (ZooStaff staff){
    Console.WriteLine("======");
    Console.WriteLine($"Id staff\t: {staff.Id}");
    Console.WriteLine($"Nama staff\t: {staff.Name}");
    Console.WriteLine($"Jabatan staff\t: {staff.Role}");
    if (staff.IsEligbleAsCaretaker()){
        Console.WriteLine("Staff Pengurus Binatang");
    }
    Console.WriteLine();
}
#endregion
List<ZooStaff> zooStaff= [new ZooStaff(1, "Budi", "CEO")];
zooStaff.Add(new ZooStaff(2,"Bambang", "CFO"));
zooStaff.Add(new ZooStaff(3, "Bila", "CTO"));
zooStaff.Add(new ZooStaff(4,"Baik", "AnimalSpecialist"));
zooStaff.Add(new ZooStaff(5,"Baihaqi", "AnimalSpecialist"));
zooStaff.Add(new ZooStaff(6, "Bayu", "AnimalSpecialist"));
int id;
string nama, role, habitat;

List<Animal> animals = new List<Animal>(){
    new Crocodile(1, "Croco", "Swamp Water"),
    new Tiger(2, "Tigger", "Land Forest"),
    new Cassowaries(3, "Dino", "Land Forest")};
char pilmenu, pilulang, pilHewan, pilCaretaker;
try{
    do{
        Console.WriteLine("Welcome to Pendataan Kebun Binatang X ");
        Console.WriteLine("Pilih apa yang ingin anda kelola");

        Console.WriteLine("\t1. Daftar Staff");
        Console.WriteLine("\t2. Daftar Hewan");
        Console.WriteLine("\t3. Tambah Staff");
        Console.WriteLine("\t4. Tambah Hewan");
        Console.WriteLine("\t5. Assign Caretaker");
        Console.WriteLine("\t6. Keluar");

        Console.Write("Pilihan menu (masukkan angka saja) : ");
        pilmenu = Console.ReadLine()[0];

        switch(pilmenu)
        {
            case '1':
                Console.WriteLine("\t1.Daftar Staff");
                for (int i = 0; i < zooStaff.Count; i++)
                {
                    PrintStaffDetail(zooStaff[i]);
                }
            break;
            case '2':
                Console.WriteLine("\t2. Daftar Hewan");
                foreach (var animal in animals)
                {
                    PrintAnimalDetail(animal);
                }
            break;
            case '3':
                Console.WriteLine("\t3. Tambah Staff");

                Console.Write("\tInput Id\t:");
                id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tInput Nama\t:");
                nama = Console.ReadLine();

                Console.Write("\tInput Role\t:");
                role = Console.ReadLine();

                var staffBaru = new ZooStaff(id, nama, role);
                zooStaff.Add(staffBaru);

                Console.WriteLine("Berhasil Tambahkan Staff Baru");
                PrintStaffDetail(staffBaru);
            break;
            case '4':
                Console.WriteLine("\t4. Tambah Hewan");

                Console.WriteLine("\tPilih jenis Hewan(angkanya saja)\t:");
                Console.WriteLine("\t\t1. Buaya / Crocodile");
                Console.WriteLine("\t\t2. Harimau / Tiger");
                Console.WriteLine("\t\t3. Kasuari / Cassowaries");
                Console.Write("\tPilihan hewan:");
                pilHewan = Console.ReadLine()[0];
                switch(pilHewan){
                    case '1':
                        Console.Write("\tInput Id\t:");
                        id = Convert.ToInt32(Console.ReadLine());

                        Console.Write("\tInput Nama\t:");
                        nama = Console.ReadLine();

                        Console.Write("\tInput Habitat\t:");
                        habitat = Console.ReadLine();
                        var buayaBaru = new Crocodile(id,nama,habitat);
                        animals.Add(buayaBaru);

                        Console.WriteLine("Berhasil Tambahkan Hewan Baru");
                        PrintAnimalDetail(buayaBaru);
                    break;
                    case '2':
                        Console.Write("\tInput Id\t:");
                        id = Convert.ToInt32(Console.ReadLine());

                        Console.Write("\tInput Nama\t:");
                        nama = Console.ReadLine();

                        Console.Write("\tInput Habitat\t:");
                        habitat = Console.ReadLine();
                        var harimauBaru = new Tiger(id,nama,habitat);
                        animals.Add(harimauBaru);

                        Console.WriteLine("Berhasil Tambahkan Hewan Baru");
                        PrintAnimalDetail(harimauBaru);
                    break;
                    case '3':
                        Console.Write("\tInput Id\t:");
                        id = Convert.ToInt32(Console.ReadLine());

                        Console.Write("\tInput Nama\t:");
                        nama = Console.ReadLine();

                        Console.Write("\tInput Habitat\t:");
                        habitat = Console.ReadLine();
                        var kasuariBaru = new Cassowaries(id,nama,habitat);
                        animals.Add(kasuariBaru);

                        Console.WriteLine("Berhasil Tambahkan Hewan Baru");
                        PrintAnimalDetail(kasuariBaru);
                    break;
                }
            break;
            case '5':
                Console.WriteLine("\t5. Assign Caretaker");
                Console.WriteLine("\tPilih hewan yang akan diassign caretaker (cukup angkanya saja)");
                for (int i = 0; i < animals.Count; i++)
                {
                    Console.WriteLine($"\t\t{i}. {animals[i].Name} si {animals[i].GetType().Name}");
                }
                Console.Write("\tPilihan hewan:");
                int angkaPilHewan = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"\tPilih Caretaker untuk {animals[angkaPilHewan].Name} si {animals[angkaPilHewan].GetType().Name}");
                for (int i = 0; i < zooStaff.Count; i++)
                {
                    Console.WriteLine($"\t\t{i}. {zooStaff[i].Name} - {zooStaff[i].Role}");
                }
                Console.Write("\tPilihan Staff Caretaker:");
                int angkaPilCaretaker = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"\t{zooStaff[angkaPilCaretaker].Name} akan diassign menjadi caretaker untuk {animals[angkaPilHewan].Name} si {animals[angkaPilHewan].GetType().Name}");
                animals[angkaPilHewan].CareTaker = zooStaff[angkaPilCaretaker];

                Console.WriteLine($"\tSukses assign {zooStaff[angkaPilCaretaker].Name} menjadi caretaker untuk {animals[angkaPilHewan].Name} si {animals[angkaPilHewan].GetType().Name}");
            break;
            case '6':
            break;
        }

        Console.Write("Ulang program (y/n) : ");
        pilulang = Console.ReadLine()[0];
    } while (pilulang is 'y' or 'Y');
}
catch (Exception ex){
    Console.WriteLine($"Terjadi kesalahan, {ex.Message}");
}
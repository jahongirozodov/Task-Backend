Console.WriteLine("Ushbu kod foydalanuvchidan qatorlar va ustunlar sonini olib, ixtiyoriy qiymatlarga ega bo'lgan ikki o'lchamli massiv yaratadi!");

Console.Write("Qatorlar soni: ");
int rows = Convert.ToInt32(Console.ReadLine());

Console.Write("Ustunlar soni: ");
int columns = Convert.ToInt32(Console.ReadLine());

// Ixtiyoriy massivni yaratish
int[,] ixtiyoriyMassiv = new int[rows, columns];
Random random = new Random();

// Massivga ixtiyoriy qiymatlar to'ldirish
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        ixtiyoriyMassiv[i, j] = random.Next(1, 100); // 1 dan 100 gacha ixtiyoriy qiymatlar
    }
}

// Ixtiyoriy massivni chiqarish
Console.WriteLine("Ixtiyoriy massiv:");
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        Console.Write(ixtiyoriyMassiv[i, j] + "\t");
    }
    Console.WriteLine();
}
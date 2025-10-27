using System;

public class Patient
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "Имя";
    public string LastName { get; set; } = "Фамилия";
    public string MiddleName { get; set; } = "Отчество";
    public DateTime Birthday { get; set; }
    public DateTime LastAppointment { get; set; }
    public int LastDoctor { get; set; } = 0;
    public string Diagnosis { get; set; } = "Диагноз";
    public string Recomendations { get; set; } = "Рекомендация";
    public Patient()
	{
	}
}

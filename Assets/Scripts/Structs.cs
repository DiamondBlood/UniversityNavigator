using Firebase.Firestore;

[FirestoreData]
public struct AdminData
{
    [FirestoreProperty]
    public string Login { get; set; }

    [FirestoreProperty]
    public string Password { get; set; }

}

[FirestoreData]
public struct SecurityData
{
    [FirestoreProperty]
    public string Fam { get; set; }

    [FirestoreProperty]
    public string Login { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Otch { get; set; }

    [FirestoreProperty]
    public string Password { get; set; }

}

[FirestoreData]
public struct EmployeeData
{
    [FirestoreProperty]
    public string Fam { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Otch { get; set; }
    [FirestoreProperty]
    public string Role { get; set; }

    [FirestoreProperty]
    public string Login { get; set; }

    [FirestoreProperty]
    public string Password { get; set; }

}

[FirestoreData]
public struct StudentData
{
    [FirestoreProperty]
    public string Fam { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Otch { get; set; }

    [FirestoreProperty]
    public string Login { get; set; }

    [FirestoreProperty]
    public string Password { get; set; }

}

[FirestoreData]
public struct ObjectData
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Type { get; set; }

    [FirestoreProperty]
    public string Description { get; set; }

    [FirestoreProperty]
    public string WorkingHours { get; set; }

}

[FirestoreData]
public struct EmergencyCoordinatesData
{
    [FirestoreProperty]
    public float X { get; set; }

    [FirestoreProperty]
    public float Y { get; set; }

    [FirestoreProperty]
    public float Z { get; set; }

}
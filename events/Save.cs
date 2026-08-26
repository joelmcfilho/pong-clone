using System;
using Godot;

public partial class Save
{
    private String _filePath = "user://svrcd.save";
    private double _recordTimeStandard = 0.0f;

    public void CheckRecord(double actualTime, double recordTime)
    {
        if(actualTime > recordTime)
        {
            SaveTimeRecord(actualTime);
            GD.Print($"DEBUG Record reached! New record in {FileAccess
                                                        .Open(_filePath,FileAccess.ModeFlags.Read)
                                                        .GetDouble()}");
        }
        else GD.Print("DEBUG Record not reached!");
        
    }

    public void SaveTimeRecord(double actualTime)
    {
        FileVerify(_filePath);

        FileAccess
        .Open(_filePath,FileAccess.ModeFlags.Write)
        .StoreDouble(actualTime);

        GD.Print($"File Saved succesfully. {_filePath}, {FileAccess
                                                        .Open(_filePath,FileAccess.ModeFlags.Read)
                                                        .GetDouble()}"
                );


    }

    public double LoadTimeRecord()
    {
        FileVerify(_filePath);

        double timeRecord = FileAccess
                            .Open(_filePath,FileAccess.ModeFlags.Read)
                            .GetDouble();

        return timeRecord;
        
    }

    public void FileVerify(String filePath)
    {
        if(!FileAccess.FileExists(filePath))
        {
            GD.Print("RECORD not found, creating new save file.");

            double _recordTimeStandard = 0.0f;

            FileAccess
            .Open(filePath,FileAccess.ModeFlags.Write)
            .StoreDouble(_recordTimeStandard);

            GD.Print($"File Saved succesfully. {filePath}, {_recordTimeStandard}");

        }
        else
        {
            GD.Print($"Savefile found! {filePath}");
        }
    }
}
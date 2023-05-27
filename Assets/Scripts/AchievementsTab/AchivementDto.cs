using System;

[System.Serializable]
public class AchievementDto
{
    public int? id;
    public string? name;
    public string? description;
    public string? creatorId;
    public string? ownerId;
    public bool isGlobal = false;
    public int? globalId;
    public int lockedIconId;
    public int unlockedIconId;
    public bool isPrivate = false;
    public ProgressType progressType;
    public bool isUnlocked;
    public int? progressTarget;
    public int? progressCurrent;
    public string? tasks;
    public string? notes;

    public enum ProgressType
    {
        Single, Multiple, Tasks, Infinite
    }
}

/*
public string? OwnerId { get; set; }
public DateTime? CreatedDateTime { get; set; }
public int? GlobalId { get; set; }
public int? ProgressTarget { get; set; }
public int? ProgressCurrent { get; set; }
public string? Tasks { get; set; }
 */
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cosmetic
{

    public string HeadId;

    public string BodyId;

    public string AttachmentId;

    /// <summary>
    /// The set id of the cosmetic
    /// </summary>
    /// <param name="id">the id to set</param>
    /// <param name="type">the type</param>
    public void SetId(string id, string type)
    {
        if (type == "head")
        {
            HeadId = id;
        }
        else if (type == "body")
        {
            BodyId = id;
        }
        else if (type == "attachment")
        {
            AttachmentId = id;
        }
        else
        {
            throw new System.Exception("Invalid type");
        }
    }

    /// <summary>
    /// get the id of the saved type
    /// </summary>
    /// <param name="type">the type to return</param>
    /// <returns></returns>
    public string GetId(string type)
    {
        if (type == "head")
        {
            return HeadId;
        }
        else if (type == "body")
        {
            return BodyId;
        }
        else if (type == "attachment")
        {
            return AttachmentId;
        }
        else
        {
            throw new System.Exception("Invalid type");
        }
    }
}

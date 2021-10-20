using System;
using System.Collections.Generic;
using UnityEngine;

namespace TongueTwister.Editor.Trees
{
	/// <summary>
	/// This class has been provided by Unity in the tree view examples and remains unedited from the source. 
	/// </summary>
    [Serializable]
    public class TreeElement
    {
	    /// <summary>
	    /// The ID of the tree element.
	    /// </summary>
    	[SerializeField] int m_ID;
	    
	    /// <summary>
	    /// The name of the tree element.
	    /// </summary>
    	[SerializeField] string m_Name;
	    
	    /// <summary>
	    /// The depth of the tree element (similar to number of parents). This defines how "indented" the row content
	    /// will appear in the editor.
	    /// </summary>
    	[SerializeField] int m_Depth;
	    
	    /// <summary>
	    /// The parent of this tree element.
	    /// </summary>
    	[NonSerialized] TreeElement m_Parent;
	    
	    /// <summary>
	    /// The children of this tree element.
	    /// </summary>
    	[NonSerialized] List<TreeElement> m_Children;

	    /// <summary>
	    /// The depth of the tree element (similar to number of parents). This defines how "indented" the row content
	    /// will appear in the editor.
	    /// </summary>
    	public int depth
    	{
    		get { return m_Depth; }
    		set { m_Depth = value; }
    	}

	    /// <summary>
	    /// The parent of this tree element.
	    /// </summary>
    	public TreeElement parent
    	{
    		get { return m_Parent; }
    		set { m_Parent = value; }
    	}

	    /// <summary>
	    /// The children of this tree element.
	    /// </summary>
    	public List<TreeElement> children
    	{
    		get { return m_Children; }
    		set { m_Children = value; }
    	}

	    /// <summary>
	    /// Whether or not there are children in this tree element.
	    /// </summary>
    	public bool hasChildren
    	{
    		get { return children != null && children.Count > 0; }
    	}

	    /// <summary>
	    /// The name of the tree element.
	    /// </summary>
    	public string name
    	{
    		get { return m_Name; } set { m_Name = value; }
    	}

	    /// <summary>
	    /// The ID of the tree element.
	    /// </summary>
    	public int id
    	{
    		get { return m_ID; } set { m_ID = value; }
    	}

    	public TreeElement ()
    	{
    	}

    	public TreeElement (string name, int depth, int id)
    	{
    		m_Name = name;
    		m_ID = id;
    		m_Depth = depth;
    	}
    }
}
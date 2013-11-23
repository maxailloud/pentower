// uScript Action Node
// (C) 2011 Detox Studios LLC

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[NodePath("Actions/Variables/Lists/Transform")]

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Get the number of things currently in the list.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]

[FriendlyName("Get List Size (Transform)", "Get the number of things currently in the list.")]
public class uScriptAct_GetListSizeTransform : uScriptLogic
{

   public bool Out { get { return true; } }

   public void In(
      [FriendlyName("Target", "The list to get the size on.")]
      Transform[] List,

      [FriendlyName("Size", "The size of the list specified.")]
      out int ListSize
      )
   {
      ListSize = List.Length;
   }

}

//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Quamotion">
//     Copyright (c) 2014 Quamotion. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("99ad444b-96e8-474e-815c-7974bec17ee2")]

// Make the internal classes visible to the Test assembly
[assembly: InternalsVisibleTo("FFmpeg.Native.Test")]
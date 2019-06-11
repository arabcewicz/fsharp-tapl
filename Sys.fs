(* https://github.com/fsprojects/FSharp.Compatibility/blob/master/FSharp.Compatibility.OCaml.System/Sys.fs *)
(*

Copyright 2005-2009 Microsoft Corporation
Copyright 2012 Jack Pappas

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*)

/// System interface.
[<CompilerMessage(
    "This module is for ML compatibility. \
    This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.",
    62, IsHidden = true)>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module FSharp.Compatibility.OCaml.Sys

#nowarn "52" // defensive value copy warning, only with warning level 4

open System
open System.Collections.Generic
open System.Diagnostics
open System.IO
//open Mono.Unix
open Microsoft.FSharp.Control

#if FX_NO_COMMAND_LINE_ARGS
#else
//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.Environment.GetCommandLineArgs directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let argv = System.Environment.GetCommandLineArgs ()
#endif

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.IO.File.Exists directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let file_exists (s : string) =
    System.IO.File.Exists s

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.IO.File.Delete directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let remove (s : string) =
    System.IO.File.Delete s

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.IO.File.Move directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let rename (s : string) (s2 : string) =
    File.Move (s, s2)

#if FX_NO_ENVIRONMENT
#else
//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.Environment.GetEnvironmentVariable directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let getenv (s : string) =
    if System.Object.ReferenceEquals (null, s) then
        raise Not_found
    else
        match Environment.GetEnvironmentVariable s with 
        | null ->
            raise Not_found
        | s -> s
#endif

#if FX_NO_PROCESS_START
#else
// Run the command and return it's exit code.
//
// Warning: 'command' currently attempts to execute the string using 
// the 'cmd.exe' shell processor.  If it is not present on the system 
// then the operation will fail.  Use System.Diagnostics.Process 
// directly to run commands in a portable way, which involves specifying 
// the program to run and the arguments independently.
[<CompilerMessage("This construct is for ML compatibility. Consider using System.Diagnostics.Process directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let command (s : string) =
    let psi =
        // Use platform-specific commands to run the specified command.
        match System.Environment.OSVersion.Platform with
        | System.PlatformID.Win32NT ->
            System.Diagnostics.ProcessStartInfo ("cmd", "/c " + s)
//        | System.PlatformID.Unix ->
//            System.Diagnostics.ProcessStartInfo ("/bin/sh", s)
        | platform ->
            let msg = sprintf "This function is not currently supported on this platform. (PlatformID = %O)" platform
            raise <| System.NotSupportedException (msg)
    
    psi.UseShellExecute <- false
    let p = System.Diagnostics.Process.Start psi
    p.WaitForExit ()
    p.ExitCode
#endif

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.IO.Directory.SetCurrentDirectory directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let chdir (s : string) =
    Directory.SetCurrentDirectory s

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.IO.Directory.GetCurrentDirectory directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let getcwd () =
    Directory.GetCurrentDirectory ()

//
[<CompilerMessage("This construct is for ML compatibility. Consider using sizeof<int> directly, where this returns a size in bytes. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let word_size = sizeof<int> * 8

#if FX_NO_PROCESS_DIAGNOSTICS
#else
// Sys.time only returns the process time from the main thread
// The documentation doesn't guarantee that thread 0 is the main thread, 
// but it always appears to be.
let private mainThread =
    lazy
        let thisProcess = Process.GetCurrentProcess ()
        let threads = thisProcess.Threads
        threads.[0]

//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.Diagnostics.Process.GetCurrentProcess().UserProcessorTime.TotalSeconds directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let time () =
    try mainThread.Force().TotalProcessorTime.TotalSeconds
    with _ ->
      // If the above failed, e.g. because main thread has exited, then do the following
      Process.GetCurrentProcess().UserProcessorTime.TotalSeconds
#endif

#if FX_NO_APP_DOMAINS
#else
//
[<CompilerMessage("This construct is for ML compatibility. Consider using System.AppDomain.CurrentDomain.FriendlyName directly. This message can be disabled using '--nowarn:62' or '#nowarn \"62\"'.", 62, IsHidden=true)>]
let executable_name =
    let currentDomain = AppDomain.CurrentDomain
    Path.Combine (
        currentDomain.BaseDirectory,
        currentDomain.FriendlyName)  
#endif

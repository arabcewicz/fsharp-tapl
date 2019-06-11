#####################################
Types and Programming Languages in F#
#####################################

Code and Examples from Benjamin Pierce's "`Types and Programming Languages`_".

.. _`Types and Programming Languages`: http://www.cis.upenn.edu/~bcpierce/tapl/


Overview
========

"`Types and Programming Languages`_" provides a comprehensive introduction to type systems and programming language theory. The code which accompanies the book is written in OCaml; this repository contains an F# port of that code.

**NOTE:** The ported F# code is not a fresh implementation -- it is the *original* OCaml code with some trivial modifications which allow it to compile with F#. The output of each of the F# projects has been verified to match the `output of the original OCaml programs`_.

**NOTE 2:** This is a fork of Jack Pappas' original F# port that has been ported to .NET Core. It contains some dependencies (i.e. _`FSharp.Compatibility.OCaml.LexYacc` and _`FSharp.Compatibility.OCaml.System`) as source code because that libraries weren't ported to .NET Standard yet (at the day of creating it).    

.. _`Types and Programming Languages`: http://www.cis.upenn.edu/~bcpierce/tapl/
.. _`output of the original OCaml programs`: fsharp-tapl/blob/master/expected-output.rst
.. _`FSharp.Compatibility.OCaml.LexYacc` : https://github.com/fsprojects/FSharp.Compatibility/tree/master/FSharp.Compatibility.OCaml.LexYacc
.. _`FSharp.Compatibility.OCaml.System`: https://github.com/fsprojects/FSharp.Compatibility/tree/master/FSharp.Compatibility.OCaml.System

# ExtendedControls
c# extended controls

Used by EDDiscovery and other programs as extended controls set

It includes the following projects:

* Extended Controls - no package dependencies, project https://github.com/EDDiscovery/BaseUtilities / BaseUtils 
* Extended Forms - no package dependencies, project https://github.com/EDDiscovery/BaseUtilities / BaseUtils + Audio, Extended Controls (relative ref)

The vsproj has had its ProjectReference for Baseutilities manually changed to use $(SolutionDir) as the base folder to find Baseutilities. So it expects Baseutilities to be checked out at c:\code\parent\Baseutilities\..

Check this module out and you can use the TestExtendedControls forms to test this extended controls

Note when you include this module in a parent module, you should not expand the baseutilities submodule.  Instead include it in the parent module itself.  

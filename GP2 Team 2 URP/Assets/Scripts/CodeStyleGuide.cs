/***************************************************************************************
 *                           CODE STYLE GUIDE AND NOTES
 ***************************************************************************************
 *
 * -------------------------------------------------------------------------------------
 *      TABLE OF CONTENTS
 * -------------------------------------------------------------------------------------
 * - CHANGE LIST
 * - FOLDER STRUCTURE AND NAMING
 * - FILE NAMING
 * - CODE STYLE
 * -------------------------------------------------------------------------------------
 * =====================================================================================
 * -------------------------------------------------------------------------------------
 *  
 * -------------------------------------------------------------------------------------
 *                                    CHANGE LIST
 * -------------------------------------------------------------------------------------
 *      DATE                NAME                    CHANGE
 *  12/01/2025          Briana Dempsey          Set up Template, 
 *                                                  and initial style proposal
 * 
 * 
 * 
 * 
 * -------------------------------------------------------------------------------------
 * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 * -------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------
 *                             FOLDER STRUCTURE AND NAMING
 * -------------------------------------------------------------------------------------
 * 
 *  - Scripts/Editor  is for any code we don't want to compile in the build
 *                      we may or may not want to further subdivide this folder 
 *                      if we make a lot of tools and utilities
 * - We need to decide on Naming conventions and how many folders we want to use
 * 
 * -------------------------------------------------------------------------------------
 * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 * -------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------
 *                                     FILE NAMING
 * -------------------------------------------------------------------------------------
 * - Monobehaviours need to be in a file that matches their class name
 * - do we want any other Naming Conventions?
 * 
 * -------------------------------------------------------------------------------------
 * +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
 * -------------------------------------------------------------------------------------
 * 
 * -------------------------------------------------------------------------------------
 *                                      CODE STYLE
 * -------------------------------------------------------------------------------------
 *          Sample taken from Microsoft's C# documentation
 *  - Use PascalCasing when naming a class, interface, struct, or delegate, methods, 
 *      local functions, and when naming public members of types, 
 *      such as fields, properties, events
 * - Use camelCasing when naming private or internal fields and prefix them with _.
 *      eg. _thisIsAPrivateVariable
 * - Use camelCase when naming local variables, including instances of a delegate type.
 *      eg. thisIsLocal
 * - Prefer clarity over brevity.
 * 
 * - Use && instead of & and || instead of |
 * - Don't use var when the type isn't apparent from the right side of the assignment. 
 *      Don't assume the type is clear from a method name. 
 *      A variable type is considered clear if it's a new operator, 
 *      an explicit cast, or assignment to a literal value.
 ***************************************************************************************/



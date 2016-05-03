Contents:
1. Project specifications
2. Project documentation info
3. Project sources


1. Project specifications

	Phase 1 - application development

		Specifications

		The application will consist in three modules:

		1. A simple database system
		definition of a file format for storing the tables
		operations on databases: creation (by defining the tables), deletion; once the database has been created, its structure (and the structure of the tables it contains) will not be modified.
		operations on tables: record insertion/deletion/update, selection
		2. A module for input/output management. For both input and output, two of the variants below must be implemented (may be different for input and output, respectively):
		web interface
		user dialog
		file - the format may be either text (tab-separated) or HTML (only if web interface is not implemented) or XML
		3. A module that carries out the operations required by the management process:
		data input
		get the academic status of a student (examination results, ECTS average, credits achieved, ...)
		classify the students, based on their results: budget-financed/fee payer
		at the end of the academic year, decide which students will be promoted to the next study year and which ones will repeat the current study year
		publish the results
		Permanent communication with the beneficiary is necessary, so feel free to ask any questions you may have about the requirements. Programs that do not do what they are supposed to, due to misunderstanding the requirements, will be penalized.

		The recommended programming languages are C# and Java. It is however allowed to choose any other language, provided it has unit testing and mocking tools, as well as assertions (which must be language-specific, apart from unit testing assertions); all these will be necessary during the next phases.

		It is recommended to design a program structure as simple as possible, without including any additional features than the ones mentioned above. The goal is to create a working version of the program, not necessarily fully stable or error-free, on which testing techniques will subsequently be applied.

		The access to the database will be low-level, not making use of specialized libraries (e.g., for parsing XML files).


	Phase 2 - unit testing

		Specifications

		Use of unit testing tools to test the code developed in phase 1.
		Reminder: unit testing is about discovering if the module being tested can handle incorrect input data (provided by other modules or by application I/O). Error fixing is NOT required.
		Testing must provide code coverage as complete as possible. For indications regarding the conditions to be tested, read the courses.
		Each module must be tested independently. For simulating the interactions with other modules, where necessary, mocking will be used.


2. Project documentation info

	The documentation for this project is provided within the solution and can be found in the folder: 4.Documentation.
	The file has been created using the LibreOffice Fresh office software suite and it is the recommended application to be
	used when reading the documentation.


3. Project sources

	The project sources can be downloaded from the following Git repository: https://github.com/EchipaProiectCSS/AcademicManager.
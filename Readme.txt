# Introduction
This is a project, is a razor pages project...... SOOO yeah.. cool

# Shortcuts
*  **Change Log Updates**
	 * [Version 0.3.0](#Version-0.1.0-14-09-2021)
	 * [Version 0.2.0](#Version-0.1.0-13-09-2021)
	 * [Version 0.1.0](#Version-0.1.0-10-09-2021)
	 
* **Infomation**
	* [How To Use](#How-To-Use)
	 * [Compatibility](#Compatibility)
	* [Methods Used](#Methods-Used)
		* [OnPostCreate](#OnPostCreate)
		* [OnPostUpdate](#OnPostUpdate)
		* [OnPostDelete](#OnPostDelete)
	* [ToDO's](#ToDO's)
	
# How To Use
* **Create new Todo**
	* Click on btn "Create New" to display modal with inputs needed to add Todo.
	Fill out the description textbox & choose priority level if needed.
* **Edit Todo**
	* Pressing on the btn "Edit" next to the "Delete" btn, will open a new page, with the associated inputs.
* **Delete Todo**
	* Click on the btn "Delete" next to "Edit" will delete the selected Todo.
* **Mark Todo as complete**
	* Check the checkbox next to the Todo description text & "Edit" btn.
	Then press save to mark them as complete.
# Compatibility
- [x]  **Oprea GX** 
- [x] **Microsoft Edge**
 - [x] **FireFox**
- [ ] **Google Chrome dosen't jiggle with the styling**
# Methods Used
### OnPostCreate
* Gets Todo object from index and adds it to the Todo lists.
### OnPostUpdate
* Gets ID from index & uses it to find the Todo, and then updates all parameters.
### OnPostDelete
* Gets ID from index & uses it to find the Todo, and then delete it from the Todo list.

# ToDO's
- [ ] Delete from load list without clearing list.
- [ ] Add an edit modal to index.
- [ ] Validation on description textbox work...
- [ ]  Add end date input box under edit.

# Change log
# [Unreleased]
## [Version 0.3.0](https://github.com/CarfloHD/Razer-H2/compare/Version-0.2.0...Version-0.3.0) 14-09-2021
### Added
- [x] Save button to check if IsComplet was check.
- [x] Load button to show completed todos.
- [x] Edit button to edit selected todo.
- [x] Edit page for editing todos.
- [x] Styling for edit page & buttons.
### Changed
- [x] Styling of entire page.
- [x]  Show todos by oldest first.
## [Version 0.2.0](https://github.com/CarfloHD/Razer-H2/compare/Version-0.1.0...Version-0.2.0) 13-09-2021
### Added
- [x] Button for opening modal.
- [x] Modal for adding Todo .
- [x] Styling for modal.
### Changed 
- [x] Styling on Todo list container.
## Version 0.1.0 10-09-2021
### Added
- [x] Class ToDo .
- [x] Todo Repository.
- [x] Method for creating, deleteting & updating.
- [x] Method for reading Todo list to index list & finding Todo by id.
- [x] Container for displaying Todo from list & styling.

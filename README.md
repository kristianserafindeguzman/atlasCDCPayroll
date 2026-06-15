# ATLAS CDC Payroll Management System

**Group 6**  
**Presented by:** Lawrence, Angelo, Kristian, Niña, Earl

## Project Overview
ATLAS CDC Payroll Management System is a web-based application designed to manage employee records and support payroll-related operations. 
Many organizations still rely on manual record keeping, which can result in errors, data redundancy, and inefficient employee management. 
Our system addresses these issues by providing a centralized platform where administrators can manage employee information, user accounts, and system records securely and efficiently.

## Objectives
- Centralize employee information
- Improve data management efficiency
- Secure user access through authentication
- Support future payroll processing functionalities
- Reduce manual record-keeping processes 

## Modules
### Login and Authentication Module
- Secure user login
- User credential validation

### Administrator Dashboard
- Centralized control panel
- Navigation to system modules

### Employee Registration
- Add new employees
- Create employee accounts

### Employee Management
- View employee records
- Delete employee records
- Manage employee information

---

## Developer Guide: How to Contribute

### 1. Prerequisites
- Download and install the latest **.NET 10 SDK**.
- An IDE like Microsoft Visual Studio 2026 or Visual Studio Code.

### 2. Forking the Repository
1. Navigate to the original GitHub repository: `https://github.com/kristianserafindeguzman/atlasCDCPayroll`
2. Click the **Fork** button in the top-right corner of the page to create a copy of the repository under your own GitHub account.

### 3. Cloning to your Machine
Open your terminal (PowerShell, Command Prompt, or Git Bash), navigate to the folder where you want to store the project, and run:
```bash
git clone https://github.com/YOUR-USERNAME/atlasCDCPayroll.git
```
*(Make sure to replace `YOUR-USERNAME` with your actual GitHub username).*

### 4. Running the System
Navigate into the inner project folder where the application code resides. The system comes with a pre-configured, portable SQLite database (`payroll.db`), so no extra database server setup is required!
```bash
cd atlasCDCPayroll/atlasCDCPayroll
dotnet run
```
Open your browser and navigate to `http://localhost:5199/`.

**Test Accounts:**
- **Administrator**: `admin` / `payroll123`
- **Manager**: `manager` / `payroll123`
- **Employee**: `ram` / `ram123`

### 5. Making Changes and Committing
Once you've added new code, modified views, or updated controllers, you need to save and upload your work back to GitHub:

```bash
# 1. Check which files were modified
git status

# 2. Stage all your new changes
git add .

# 3. Commit your changes with a short, descriptive message
git commit -m "Added a detailed description of what I changed"

# 4. Push the changes up to your GitHub fork
git push origin main
```

**Submitting for Review:**
After pushing your changes, go to your GitHub fork repository in your web browser and click the **"Compare & pull request"** button to submit your changes to the main group repository so your teammates can review and merge them!
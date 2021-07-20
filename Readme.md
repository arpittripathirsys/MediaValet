# Prerequisite

- .Net Core 3.1
- Visual Studio 2019

# Steps To Test:

- Run Azure Storage Emulator
- Open Solution in Visual Studio 2019 
- Right click on OrderAgent project and select option "Debug->Start New Instance". Do this activity multiple times to run multiple agents.
- Right click on OrderSupervisor project and select option "Debug->Start New Instance".
- Right click on TestClient project and select option "Debug->Start New Instance".
- Enter order text in test client window and click on submit button. Response will be displayed against "Response" label.


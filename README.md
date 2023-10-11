# Blockchain Event Stream Processor

**Blockchain Event Stream Processor** is a system designed to handle and process token related requests.

## Features

- Mint token to address
- Update token owners
- Burn tokens
- Reset database
- Query database

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 or later)

### Installation

1. Clone the repository to your local machine.
2. Build the project.
3. Run the application.

## Usage

The system supports various commands for interacting with the blockchain. Use the following commands to perform specific actions:

- **Read Inline (--read-inline <json>)**: Reads either a single JSON element or an array of JSON elements representing transactions as an argument.
  ```bash
  dotnet run -- --read-inline '{"Type": "Burn", "TokenId": "0x..."}'
  dotnet run -- --read-inline '[{"Type": "Mint", "TokenId": "0x...", "Address": "0x..."}, {"Type": "Burn", "TokenId": "0x..."}]'

- **Read File (--read-file <file>)**: Reads either a single JSON element or an array of JSON elements representing transactions from the file in the specified location.
  ```bash
  dotnet run -- --read-file transactions.json

- **Wallet Ownership (--wallet <address>)**: Lists all NFTs currently owned by the wallet of the given address.
  ```bash
  dotnet run -- --wallet 0x...

- **Reset (--reset)**: Deletes all data previously processed by the program.

## Sample Input / Output

### Input
Given the file `transactions.json` with the following contents:

  [
      {
          "Type": "Mint",
          "TokenId": "0xA000000000000000000000000000000000000000",
          "Address": "0x1000000000000000000000000000000000000000"
      },
      {
          "Type": "Mint",
          "TokenId": "0xB000000000000000000000000000000000000000",
          "Address": "0x2000000000000000000000000000000000000000"
      },
      {
          "Type": "Mint",
          "TokenId": "0xC000000000000000000000000000000000000000",
          "Address": "0x3000000000000000000000000000000000000000"
      },
      {
          "Type": "Burn",
          "TokenId": "0xA000000000000000000000000000000000000000"
      },
      {
          "Type": "Transfer",
          "TokenId": "0xB000000000000000000000000000000000000000",
          "From": "0x2000000000000000000000000000000000000000",
          "To": "0x3000000000000000000000000000000000000000"
      }
  ]

### Output

1. `program --read-file transactions.json`
   - Result: Read 5 transaction(s)

2. `program --nft 0xA000000000000000000000000000000000000000`
   - Result: Token 0xA000000000000000000000000000000000000000 is not owned by any wallet

3. `program --nft 0xB000000000000000000000000000000000000000`
   - Result: Token 0xA000000000000000000000000000000000000000 is owned by 0x3000000000000000000000000000000000000000

4. `program --nft 0xC000000000000000000000000000000000000000`
   - Result: Token 0xC000000000000000000000000000000000000000 is owned by 0x3000000000000000000000000000000000000000

5. `program --nft 0xD000000000000000000000000000000000000000`
   - Result: Token 0xA000000000000000000000000000000000000000 is not owned by any wallet

6. `program --read-inline '{ "Type": "Mint", "TokenId": "0xD000000000000000000000000000000000000000", "Address": "0x1000000000000000000000000000000000000000" }’`
   - Result: Read 1 transaction(s)

7. `program --nft 0xD000000000000000000000000000000000000000`
   - Result: Token 0xA000000000000000000000000000000000000000 is owned by 0x1000000000000000000000000000000000000000

8. `program --wallet 0x300000000000000000000000000000000000000`
   - Result: Wallet 0x300000000000000000000000000000000000000 holds 2 Tokens:
     - 0xB000000000000000000000000000000000000000
     - 0xC000000000000000000000000000000000000000

9. `program —reset`
   - Result: Program was reset

10. `program --wallet 0x300000000000000000000000000000000000000`
    - Result: Wallet 0x300000000000000000000000000000000000000 holds no Tokens
      
# Testing
Testing is an essential part of the Blockchain Event Stream Processor to ensure that it works correctly and efficiently. We use [testing frameworks](https://your-testing-framework-link) to write and run tests for the system.
markdown
Copy code
## Prerequisites

Before running the tests, make sure you have the following prerequisites in place:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 or later)
- [NUnit](https://your-testing-framework-link) (3.13.3)

## Running Tests

To run the tests, follow these steps:

1. Navigate to the root directory of the Blockchain Event Stream Processor.

2. Open a terminal or command prompt.

3. Use the following command to run the tests:

   ```bash
   dotnet test
This command will execute the tests in the project and provide a summary of the test results.

Review the test results and ensure that all tests pass without errors or failures.

If any tests fail, investigate the issues and make the necessary code adjustments.

## Writing Tests

If you need to write additional tests, you can find the test files in the [BlockChainEventStreamTest] project. Here are the steps to write and add tests:

1. Create a new test file in the appropriate folder.

2. Write test methods to cover specific functionality or scenarios in the Blockchain Event Stream Processor.

3. Run the tests using the `dotnet test` command as described above.

4. Review the test results and make any necessary adjustments to ensure the tests pass.

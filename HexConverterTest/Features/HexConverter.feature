Feature: HexConverter
A fast hex converter utility using dictionaries

Scenario Outline: Byte Array To Hex
	Given I have byte array <bytes>
	When I convert the byte array to hex
	Then the result string is <hex>

	Examples:
	| bytes       | hex      |
	| 42,84,255,0 | 2A54FF00 |
	| 255         | FF       |

Scenario: Null Byte Array To Hex
	Given I have a null byte array
	When I attempt to convert the byte array to hex
	Then an exception was thrown
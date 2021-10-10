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
	When I convert the byte array to hex
	Then an exception was thrown

Scenario Outline: Byte Array To Hex Array
	Given I have byte array <bytes>
	When I convert the byte array to a hex array
	Then the result string array is <hexArray>

	Examples:
	| bytes       | hexArray    |
	| 42,84,255,0 | 2A,54,FF,00 |
	| 255         | FF          |

Scenario Outline: String Array To Byte Array
	Given I have string array <hexArray>
	When I convert the string array to a byte array
	Then the result byte array is <bytes>

	Examples:
	| hexArray    | bytes       |
	| 2A,54,FF,00 | 42,84,255,0 |
	| FF          | 255         |

Scenario Outline: Invalid String Array To ByteArray
	Given I have string array <hexArray>
	When I convert the string array to a byte array
	Then an exception was thrown

	Examples:
	| hexArray    |
	| 2A,54,F,00  |
	| FX          |

Scenario: Null String Array To ByteArray
	Given I have a null string array
	When I convert the string array to a byte array
	Then an exception was thrown

Scenario Outline: String To Byte Array
	Given I have hex string <hex>
	When I convert the string to a byte array
	Then the result byte array is <bytes>

	Examples:
	| hex      | bytes       |
	| 2A54FF00 | 42,84,255,0 |
	| FF       | 255         |

Scenario Outline: Invalid String To ByteArray
	Given I have hex string <hex>
	When I convert the string to a byte array
	Then an exception was thrown

	Examples:
	| hex      |
	| 2A54F00  |
	| FX       |

Scenario: Null String To ByteArray
	Given I have a null string
	When I convert the string to a byte array
	Then an exception was thrown
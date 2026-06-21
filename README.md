# GIBS.Module.DateTimeCalc

This module provides date/time calculation functionality for Oqtane.

## .NET 10 Upgrade Notes

- Server, Client, and Shared projects now target **.NET 10** (`net10.0`).
- Package references were aligned to the **10.x** stack (for example `Microsoft.AspNetCore.*` and `Microsoft.EntityFrameworkCore` at `10.0.9`).
- Oqtane package references are set to **10.2.1**.
- Project reference hint paths now point to `net10.0` build outputs.
- The Package project currently targets `net9.0`; keep this in sync with your packaging/runtime requirements if you move it to .NET 10 as well.

## Build Notes

- Build the referenced Oqtane framework first so local `HintPath` references resolve.
- Then build this solution from the root:
  - `GIBS.Module.DateTimeCalc.slnx`

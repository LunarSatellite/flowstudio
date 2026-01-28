#!/bin/bash

# Aurora FlowStudio - Database Migration Script
# This script creates and applies EF Core migrations

echo "======================================"
echo "Aurora FlowStudio - Database Migration"
echo "======================================"
echo ""

# Check if migration name is provided
if [ -z "$1" ]; then
    MIGRATION_NAME="InitialCreate"
    echo "No migration name provided. Using default: $MIGRATION_NAME"
else
    MIGRATION_NAME="$1"
    echo "Migration name: $MIGRATION_NAME"
fi

echo ""
echo "Step 1: Adding migration..."
dotnet ef migrations add $MIGRATION_NAME --project Aurora.FlowStudio.Data.csproj --verbose

if [ $? -ne 0 ]; then
    echo ""
    echo "❌ Error: Failed to add migration"
    exit 1
fi

echo ""
echo "✅ Migration added successfully!"
echo ""
echo "Step 2: Applying migration to database..."
dotnet ef database update --project Aurora.FlowStudio.Data.csproj --verbose

if [ $? -ne 0 ]; then
    echo ""
    echo "❌ Error: Failed to apply migration"
    exit 1
fi

echo ""
echo "✅ Database updated successfully!"
echo ""
echo "======================================"
echo "Migration completed!"
echo "======================================"

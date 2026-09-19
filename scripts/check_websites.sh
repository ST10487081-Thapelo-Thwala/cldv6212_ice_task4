#!/bin/bash
# check_websites.sh - checks whether each website in the list is reachable

websites=(
    "https://www.google.com"
    "https://www.github.com"
    "https://www.docker.com"
    "https://nonexistent.example.invalid"
)

for site in "${websites[@]}"; do
    http_code=$(curl -s -o /dev/null -w "%{http_code}" --max-time 5 "$site")

    if [[ "$http_code" -ge 200 && "$http_code" -lt 400 ]]; then
        echo "[UP]   $site (HTTP $http_code)"
    else
        echo "[DOWN] $site (HTTP $http_code)"
    fi
done

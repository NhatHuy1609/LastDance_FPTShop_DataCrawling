## Installation

### Windows

```bash
# Create virtual environment
python -m venv env

# Activate virtual environment
env\Scripts\activate

# Copy .env.example to .env
copy .env.example .env

# Install dependencies
pip install -r requirements.txt
```

### Linux

```bash
# Create virtual environment
python -m venv env

# Activate virtual environment
source env/bin/activate

# Copy .env.example to .env
cp .env.example .env

# Install dependencies
pip install -r requirements.txt
```

## Run

```bash
python main.py
```


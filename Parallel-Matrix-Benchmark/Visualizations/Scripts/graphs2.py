import pandas as pd
from pathlib import Path
import matplotlib.pyplot as plt

csv_path = Path(__file__).resolve().parents[2] / 'bin' / 'Release' / 'net10.0' / 'benchmark_results.csv'
df = pd.read_csv(csv_path)

sizes_to_plot = [50, 100, 500, 1000, 10000]
threads_to_plot = [1, 6, 12, 24, 48, 12*8, 12*16]

baseline = df[df['Threads'] == 1].set_index('Size')['TotalTime_ms'].to_dict()

filtered_df = df[df['Threads'].isin(threads_to_plot) & df['Size'].isin(sizes_to_plot)]

fig, (ax1, ax2) = plt.subplots(1, 2, figsize=(14, 6))

colors = plt.cm.tab10.colors

x_positions = range(len(sizes_to_plot))
x_labels = [str(s) for s in sizes_to_plot]

for i, threads in enumerate(threads_to_plot):
    data = filtered_df[filtered_df['Threads'] == threads].sort_values('Size')
    ax1.plot(x_positions, data['TotalTime_ms'], 'o-',
             label=f'{threads} threads', color=colors[i], linewidth=2, markersize=5)

ax1.set_xlabel('Matrix Size (N×N)', fontsize=12)
ax1.set_ylabel('Execution Time (ms)', fontsize=12)
ax1.set_title('Effect of Data Size on Performance', fontsize=14)
ax1.legend(title='Threads', loc='upper left')
ax1.grid(True, alpha=0.3)
ax1.set_xticks(x_positions)
ax1.set_xticklabels(x_labels)

import numpy as np

bar_sizes = [10000, 15000, 20000, 25000, 30000]
optimal_threads = 12  #Best performing thread count

sequential_times = []
parallel_times = []

for size in bar_sizes:
    seq_row = df[(df['Size'] == size) & (df['Threads'] == 1)]
    par_row = df[(df['Size'] == size) & (df['Threads'] == optimal_threads)]

    sequential_times.append(seq_row['TotalTime_ms'].values[0] if not seq_row.empty else 0)
    parallel_times.append(par_row['TotalTime_ms'].values[0] if not par_row.empty else 0)

x = np.arange(len(bar_sizes))
width = 0.35

bars1 = ax2.bar(x - width/2, sequential_times, width, label='Sequential (1 thread)', color=colors[0])
bars2 = ax2.bar(x + width/2, parallel_times, width, label=f'Parallel ({optimal_threads} threads)', color=colors[2])

ax2.set_xlabel('Matrix Size (N×N)', fontsize=12)
ax2.set_ylabel('Execution Time (ms)', fontsize=12)
ax2.set_title('Sequential vs Parallel Performance on Big Data', fontsize=14)
ax2.set_xticks(x)
ax2.set_xticklabels([str(s) for s in bar_sizes])
ax2.legend(loc='upper left')
ax2.grid(True, alpha=0.3, axis='y')

plt.tight_layout()
plt.savefig('graphs2.png', dpi=150, bbox_inches='tight')
plt.show()

print("Saved to graphs2.png")
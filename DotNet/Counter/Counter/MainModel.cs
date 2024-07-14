using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counter
{
    internal partial record class MainModel
    {
        public IState<Countable> Countable => State.Value(this, () => new Countable(0, 1));

        public ValueTask IncrementCounter() => Countable.UpdateAsync(c => c?.Increment());
    }

    internal partial record Countable(int Count, int Step)
    {
        public Countable Increment () => this with
        {
            Count = Count + Step
        };
    }

}
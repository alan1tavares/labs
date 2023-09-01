// See https://aka.ms/new-console-template for more information

Guid id  = Guid.Empty;

Console.WriteLine(id);

ArgumentNullException.ThrowIfNullOrEmpty(id.ToString());
ArgumentNullException.ThrowIfNullOrEmpty(null);
